using Business_Logic.Data_Manipulations.Database_Entites;
using DataService;
using DataService.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Mappers;
using System.Data.SqlClient;
using Business_Logic.Properties;
using System.Data;


namespace Business_Logic.Handlers
{
    public class TimestampAutoCompleteHandler
    {
        private readonly UnitOfWork _uow;
        public TimestampAutoCompleteHandler()
        {
            _uow = new UnitOfWork(new VismaEntities());
        }

        public TimestampingViewModel GetAutoCompleteValues()
        {
            return new TimestampingViewModel
            {
                Customer = GetCustomers(),
                Occupations = GetOccupations(),
                Activities = GetActivities(),
                Taxes = GetTaxes(),
                Projects = GetProjects(),
            };

        }

        private List<Customer> GetCustomers()
        {
            var terms = new[] { "***", "anv ej", "anvand ej", "använd ej" };

            var actorList = new List<Actor>();
            actorList = _uow.ActorRepository.Get(filter: x => x.CustNo > 0).ToList(); ;

            foreach (var item in terms)
            {
                actorList = actorList.Where(x => !x.Nm.Contains(item)).ToList();
            }

            return actorList.ToCustomers();
        }

        private List<WorkText> GetOccupations()
        {
            return _uow.TypeOfWorkTextRepository.Get(filter: x => x.Lang == 752 && x.TxtTp == 72 && x.Txt1 != "Frånvaro" && x.Txt1 != "Interna Möten").ToContracts();
        }

        private List<Activity> GetActivities()
        {
            return _uow.ActivityRepository.Get(filter: x => x.StSaleUn == 3).ToContracts();
        }

        private List<Tax> GetTaxes()
        {
            return _uow.TypeOfWorkTextRepository.Get(filter: x => x.Lang == 752 && x.TxtTp == 73).ToTaxesContracts();
        }

        private List<Project> GetProjects()
        {
            return _uow.ProjectRepository.Get(filter: x => x.St == 2).OrderBy(c => c.Nm).ToContracts();
        }

        public List<Service> GetServices(int contractId)
        {

            var con = new SqlConnection(Settings.Default.VismaConnection);

            con.Open();

            var command = "Select distinct ordln.r8 as ServiceId, RIGHT(r8.nm, datalength(r8.nm)-3) As Name From ordln" +
                            " Inner join r8" +
                            " On  ordln.r8 = r8.rno" +
                            " Inner join Prod" +
                            " On ordln.prodno = prod.prodno" +
                            " Where ordln.R5 = @contractId AND NoInvoAB > 0" +
                            " Order by name ";

            var sqlCommand = new SqlCommand(command, con);

            sqlCommand.Parameters.Add("@contractId", SqlDbType.Int).Value = contractId;

            var usedServices = new List<Service>();

            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    usedServices.Add(new Service
                    {
                        Description = reader["Name"].ToString(),
                        Id = reader["ServiceId"].ToString(),
                        Used = true
                    });
                }
            }
            con.Close();

            var unUsedServices = _uow.ServiceRepository.GetAll().ToContracts();

            foreach (var item in unUsedServices)
            {
                item.Description = item.Description.Substring(3);
                item.Used = false;
            }

            foreach (var item in usedServices)
            {
                var service = unUsedServices.FirstOrDefault(x => x.Id == item.Id);
                if (service != null)
                {
                    unUsedServices.Remove(service);
                    unUsedServices.Add(item);
                };
            }

            return unUsedServices.OrderByDescending(c => c.Used).ToList();
        }

        public List<Agreement> GetAgreements(int customerId)
        {
            var con = new SqlConnection(Settings.Default.VismaConnection);
            con.Open();

            var command = "Select ordno[Order], r5.nm[Avtal.Namn], ord.r5[Avtal], txt.txt[Fakturatyp] from ord " +
                          " Inner Join r5" +
                          " On ord.r5 = r5.rno" +
                          " Inner Join txt" +
                          " On ord.gr = txt.txtNo" +
                          " Where ord.custno = @customerId AND r5 > 0 AND ordpref > 0 AND TxtTp = 48 AND r5.nm != '' AND txt.lang = 752 and Ord.gr3 = 0" +
                          " UNION" +
                          " Select ordno, ord.inf, ord.r5, txt.txt  from ord" +
                          " Inner Join txt" +
                          " On ord.gr = txt.txtNo" +
                          " Where ord.custno = @customerId AND ordpref > 0 AND r5 = 0 AND TxtTp = 48 AND ord.inf != '' AND txt.lang = 752 and Ord.gr3 = 0" +
                          " order by [Fakturatyp], [Avtal.Namn]";

            var sqlCommand = new SqlCommand(command, con);
            sqlCommand.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;

            var agreements = new List<Agreement>();

            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    agreements.Add(new Agreement
                    {
                        Name = reader["Avtal.Namn"].ToString(),
                        Id = Convert.ToInt32(reader["Order"]),
                        ContractId = Convert.ToInt32(reader["Avtal"])
                    });
                }
            }
            con.Close();

            return agreements;

        }


    }
}

