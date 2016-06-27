using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using Business_Logic.Properties;

namespace Business_Logic.Handlers
{
    public class InsertTimestampHandler : Config.Config
    {
        private static readonly Mutex Mutex = new Mutex();
        private readonly SqlConnection _connection;
        private readonly string _username;
        private SqlCommand _cmd;

        public InsertTimestampHandler(string username)
        {
            _connection = new SqlConnection(Settings.Default.VismaConnection);
            _username = username;
        }      

        public int GetAgractno()
        {
            // Set up a command
            var commandText = "Select actno From Actor " +
                              "Where usr = @user";

            return SelectIntValueWithUser(commandText);
        }

        private int GetAgrno()
        {
            var agractno = GetAgractno();
            var value = -1;
            var commandText = "Select max(Agrno) From Agr " +
                              "Where Agractno = @Agractno";

            _cmd = new SqlCommand(commandText, _connection);

            _cmd.Parameters.Add("@Agractno", SqlDbType.Int).Value = agractno;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                    
                {
                    while (reader.Read()) 
                    {
                        if (!string.IsNullOrEmpty(reader[0].ToString()))
                        {
                            value = Convert.ToInt32(reader[0]);
                            value += 1;
                        }
                        else
                            value = 1;
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }


        private int GetLiaactno(int orderNo)
        {
   
            var commandText = "Select liaactno From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private int GetCur(int orderNo)
        {
            var commandText = "Select cur From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private int GetExrt(int orderNo)
        {
            var commandText = "Select exrt From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private int GetInvocust(int orderNo)
        {
            var commandText = "Select invocust From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private int GetOrdlnno()
        {
            return 0;
        }

        private int GetUn(string prodNo)
        {
            var commandText = "Select stsaleun From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetPrm1(string prodNo)
        {
            var commandText = "Select procmt From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetPrm2(string prodNo)
        {
            var commandText = "Select excprint From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetPrm3(string prodNo)
        {
            var commandText = "Select editpref From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetPrm4(string prodNo)
        {
            var commandText = "Select edfmt From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetPrm5(string prodNo)
        {
            var commandText = "Select expstr From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private int GetAgrproc(string prodNo)
        {
            var commandText = "Select agrproc From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectIntValueWithProdNo(commandText, prodNo);
        }

        private string GetWagesrt(string prodNo)
        {
            var commandText = "Select wagesrt From Prod " +
                              "Where ProdNo = @prodNo";

            return SelectStringValueWithProdNo(commandText, prodNo);
        }

        private int GetCustno(string customerName)
        {
            var value = -1;

            // Set up a command
            var commandText = "Select custno From Actor " +
                              "Where nm = @customer";

            _cmd = new SqlCommand(commandText, _connection);

            /* Set the param */
            _cmd.Parameters.Add("@customer", SqlDbType.VarChar).Value = customerName;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                   
                {
                    while (reader.Read())
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetActno(int custNo)
        {
    
            var commandText = "Select actno From Actor " +
                              "Where CustNo = @custNo";

            return SelectIntValueWithCustNo(commandText, custNo);
        }

        private int GetLocale()
        {

            var commandText = "Select r1 From Actor " +
                              "Where usr = @user";

            return SelectIntValueWithUser(commandText);
        }


        //TODO FIX THIS 
        private int GetEmpno()
        {
   
            var commandText = "Select empno From Actor " +
                              "Where usr = @user";

            //return selectIntValueWithUser(commandText);
            return 15;
        }


        private double GetCstpr(string prodNo, bool debit)
        {
            double costPrice = 0;
            var empNo = GetEmpno();
          

        
            var commandText = "Select cstpr From Prdcmat " +
                              "Where empno = @empno and prodno = @prodno and cstpr != 0 and custprgr = 0";
            _cmd = new SqlCommand(commandText, _connection);

          
            _cmd.Parameters.Add("@empno", SqlDbType.Int).Value = empNo;
            _cmd.Parameters.Add("@prodno", SqlDbType.VarChar).Value = prodNo;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                
                {
                    while (reader.Read())
                    {
                        costPrice = Convert.ToDouble(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }


            return costPrice;
        }

        private double GetCcstpr(string prodNo, bool debit)
        {
            return GetCstpr(prodNo, debit);
        }

        private double GetCinccst(string prodNo, bool debit, double timeInHour)
        {
            var costPr = GetCstpr(prodNo, debit);
            return timeInHour*costPr;
        }

        private double GetInccst(string prodNo, bool debit, double timeInHour)
        {
            return GetCinccst(prodNo, debit, timeInHour);
        }

        private double GetPrice(int custNo, string prodNo, bool debit, int contract, double timeInHour, int taxa,
            int ordNo)
        {
            var customerDt = new DataTable();
           
            var minDebit = GetMinam(custNo, prodNo, debit);

            var commandText = "Select salepr, CustNo, R5,ordNo From Prdcmat " +
                              "Where prodno = @prodNo and salepr > 0  and empno = 0 and CustPrG2 = @CustPrG2";
            _cmd = new SqlCommand(commandText, _connection);

         
            _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;
            _cmd.Parameters.Add("@CustPrG2", SqlDbType.Int).Value = taxa;
        
            try
            {
              
                using (var reader = _cmd.ExecuteReader())
                {
                    customerDt.Load(reader);
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }

            double salePrice = 0;
  
            var results = customerDt.Select("CustNo = " + custNo + " AND R5 = " + contract);
            if (results.Any())
            {
                salePrice = Convert.ToDouble(results[0]["SalePr"]);
            }

            results = customerDt.Select("CustNo = " + custNo);
            if (results.Any())
            {
                salePrice = Convert.ToDouble(results[0]["SalePr"]);
            }

            results = customerDt.Select("R5 = " + contract);
            if (results.Any())
            {
                salePrice = Convert.ToDouble(results[0]["SalePr"]);
            }

            results = customerDt.Select("OrdNo = " + ordNo);
            if (results.Any())
            {
                salePrice = Convert.ToDouble(results[0]["SalePr"]);
            }

            results = customerDt.Select("CustNo = 0 AND R5 = 0");
            if (results.Any())
            {
                salePrice = Convert.ToDouble(results[0]["SalePr"]);
            }

            return CheckMinam(salePrice, timeInHour, minDebit);
        }

        private double GetDpr(int custNo, string prodNo, bool debit, int contract, double timeInHour, int taxa,
            int ordNo)
        {
            return GetPrice(custNo, prodNo, debit, contract, timeInHour, taxa, ordNo);
        }

        private double GetAm(int custNo, string prodNo, bool debit, int contract, double timeInHour,
            bool calcWithDiscount, int taxa, int ordNo)
        {
            var salePr = GetPrice(custNo, prodNo, debit, contract, timeInHour, taxa, ordNo);
            var sum = timeInHour*salePr;
            var minDebit = GetMinam(custNo, prodNo, debit);

            if (minDebit > 0) 
            {
                if (sum < minDebit) 
                    sum = minDebit;
            }

            if (!calcWithDiscount) return sum;
            var summaWithDiscount = sum*
                                    (GetDc1P(prodNo, debit, GetRightCustprgr("custprgr", custNo), custNo, contract)/100);
            sum -= summaWithDiscount;

            return sum;
        }

        private double GetDam(int custNo, string prodNo, bool debit, int contract, double timeInHour, int taxa,
            int ordNo)
        {
            return GetAm(custNo, prodNo, debit, contract, timeInHour, false, taxa, ordNo);
        }

        private double GetMinam(int custNo, string prodNo, bool debit)
        {
            double value = 0;
            var debitCode = GetDebitCode(debit);

            var commandText = "Select minam From Prdcmat " +
                              "Where custNo = @custNo and prodno = @prodNo and prodprgr = @prodprgr and salepr > 0  and empno = 0";
            _cmd = new SqlCommand(commandText, _connection);

            /* Set the param */
            _cmd.Parameters.Add("@custNo", SqlDbType.Int).Value = custNo;
            _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;
            _cmd.Parameters.Add("@prodprgr", SqlDbType.Int).Value = debitCode;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }

            if (value == 0)
            {
                commandText = "Select minam From Prdcmat " +
                              "Where prodno = @prodNo and prodprgr = @prodprgr and salepr > 0  and empno = 0";
                _cmd = new SqlCommand(commandText, _connection);


                _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;
                _cmd.Parameters.Add("@prodprgr", SqlDbType.Int).Value = debitCode;

                try
                {
                    using (var reader = _cmd.ExecuteReader())
                       
                    {
                        while (reader.Read())
                        {
                            value = Convert.ToInt32(reader[0]);
                        }
                        reader.Close(); 
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return value;
        }

        private double GetDc1P(string prodNo, bool debit, int custprgr, int custNo, int contract)
        {
            var customerDt = new DataTable();
            var debitCode = GetDebitCode(debit);
            double value = 0;

            var commandText = "Select saledcp, CustNo, R5 From Prdcmat " +
                              "Where prodno = @prodNo and prodprgr = @prodprgr and custprgr = @custprgr and empno = 0";
            _cmd = new SqlCommand(commandText, _connection);

         
            _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;
            _cmd.Parameters.Add("@prodprgr", SqlDbType.Int).Value = debitCode;
            _cmd.Parameters.Add("@custprgr", SqlDbType.Int).Value = custprgr;

            try
            {
            
                using (var reader = _cmd.ExecuteReader())
                {
                    customerDt.Load(reader);
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }

          
            var results = customerDt.Select("CustNo = " + custNo + " AND R5 = " + contract);
            if (results.Any())
            {

                return Convert.ToDouble(results[0]["SaleDcP"]);
            }

            results = customerDt.Select("CustNo = " + custNo);
            if (results.Any())
            {
     
                return Convert.ToDouble(results[0]["SaleDcP"]);
            }

            results = customerDt.Select("R5 = " + contract);
            if (results.Any())
            {

                return Convert.ToDouble(results[0]["SaleDcP"]);
            }

            results = customerDt.Select("CustNo = 0 AND R5 = 0");
            if (results.Any())
            {
 
                return Convert.ToDouble(results[0]["SaleDcP"]);
            }

            if (debit == false)
                value = 100;

            return value; 
        }

        private static double CheckMinam(double salePrice, double timeInHour, double minDebit)
        {
            if (timeInHour == 0) return 0;
            if (salePrice*timeInHour < minDebit)
                return minDebit/timeInHour;
            return salePrice;
        }

        private static float GetNoregdy(float workedTime)
        {
            return workedTime/60;
        }


        private int GetSrt()
        {
            var value = 0;
            var empNo = GetEmpno();

            var commandText = "select max(srt) from agr " +
                              "where empno = @empno";
            _cmd = new SqlCommand(commandText, _connection);

            /* Set the param */
            _cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = empNo;

            try
            {
                /* Reads the result */
                using (var reader = _cmd.ExecuteReader())
                {
                    while (reader.Read()) //Read the message and saves it
                    {
                        if (!string.IsNullOrEmpty(reader[0].ToString()))
                        {
                            value = Convert.ToInt32(reader[0]);
                            value += 1;
                        }
                        else
                            value = 1;
                    }
                    reader.Close(); //Message reader close
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetRefno()
        {
            var value = 0;

            var commandText = "select max(refno) from agr";
            _cmd = new SqlCommand(commandText, _connection);

            try
            {
                /* Reads the result */
                using (var reader = _cmd.ExecuteReader())
                {
                    while (reader.Read()) //Read the message and saves it
                    {
                        if (!string.IsNullOrEmpty(reader[0].ToString()))
                        {
                            value = Convert.ToInt32(reader[0]);
                            value += 1;
                        }
                        else
                            value = 1;
                    }
                    reader.Close(); //Message reader close
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetDupl()
        {
            return 0;
        }

        private int GetDup2()
        {
            return 0;
        }

        private int GetLckst()
        {
            return 0;
        }

        private int GetChprc()
        {
            return 0;
        }

        private int GetAcyrpr(int frdt)
        {
            return Convert.ToInt32(frdt.ToString().Substring(0, 6));
        }

        private int GetCractno()
        {
            return GetAgractno();
        }

        private string GetCreusr()
        {
            return _username;
        }

        private int GetCredt()
        {
            return Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        }

        private DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        private int GetCretm(DateTime time)
        {
            return Convert.ToInt32(time.ToString("HHmm"));
        }

        /* First time - use CREATE not change */

        private int GetChdt()
        {
            return GetCredt();
        }

        private int GetChtm(DateTime time)
        {
            return GetCretm(time);
        }

        private int GetChtmms(DateTime chTime)
        {
            return Convert.ToInt32(chTime.TimeOfDay.TotalMilliseconds);
        }

        private string GetChusr()
        {
            return _username;
        }


        private int GetDebitCode(bool debit)
        {
            return debit ? DebiteraCode : DebitEjCode;
        }

        private int GetAdwage1(bool adWage)
        {
            return adWage ? AdWTrue : AdWFalse;
        }

        public int GetInvo(bool utlagg)
        {
            return utlagg ? InvoUtl : InvoReg;
        }

        private int GetPri(bool defaultActivity)
        {
            return defaultActivity ? NPriAct : OPriAct;
        }

        private int GetCacset(int orderNo)
        {
            // Set up a command
            var commandText = "Select acset From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private int GetFin()
        {
            return Fin;
        }

        private int GetRightRInt(string currentR, int contract)
        {
            var rType = GetRType(currentR);

            if (string.IsNullOrEmpty(rType))
                return 0;
            switch (rType)
            {
                case "Locale":
                    return GetLocale();
                case "Contract":
                    return contract;
            }
            return 0;
        }

        private string GetRightRString(string currentR, string service, string project)
        {
            var rType = GetRType(currentR);

            if (string.IsNullOrEmpty(rType))
                return string.Empty;
            switch (rType)
            {
                case "Service":
                    return service;
                case "Project":
                    return project;
            }
            return string.Empty;
        }

        private int GetRightProdprgr(string currentProdprgr, bool debit, bool utlagg, string activity)
        {
            var prodType = GetProdPrType(currentProdprgr);

            if (string.IsNullOrEmpty(prodType))
                return -1;
            switch (prodType)
            {
                case "Debit":
                {
                    if (debit)
                        return 0;
                    return 1;
                }
                case "Utlagg":
                {
                    if (utlagg)
                        return 1;
                    return 0;
                }
                case "Activity":
                {
                    var value = -1;

           
                    var commandText = "select txtno from Txt " +
                                      "where txttp = @activityCode and lang = @lang and txt = @activity";
                    _cmd = new SqlCommand(commandText, _connection);

           
                    _cmd.Parameters.Add("@activity", SqlDbType.VarChar).Value = activity;
                    _cmd.Parameters.Add("@lang", SqlDbType.Int).Value = Lang;
                    _cmd.Parameters.Add("@activityCode", SqlDbType.Int).Value = ActivityId;

                    try
                    {
                        using (var reader = _cmd.ExecuteReader())
           
                        {
                            while (reader.Read()) 
                            {
                                value = Convert.ToInt32(reader[0]);
                            }
                            reader.Close(); 
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    return value;
                }
                case "":
                {
                 
                    return 0;
                }
            }
            return -1;
        }

        private int GetRightCustprgr(string currentCustprgr, int custNo)
        {
            var custType = GetCustPrType(currentCustprgr);

            if (string.IsNullOrEmpty(custType))
                return 0;
            switch (custType)
            {
                case "Price1":
                {
          
                    var commandText = "Select custprgr From Actor " +
                                      "Where CustNo = @custNo";

                    return SelectIntValueWithCustNo(commandText, custNo);
                }
                case "Price2":
                {
                  
                    var commandText = "Select custprg2 From Actor " +
                                      "Where CustNo = @custNo";

                    return SelectIntValueWithCustNo(commandText, custNo);
                }
                case "Price3":
                {
                  
                    var commandText = "Select custprg3 From Actor " +
                                      "Where CustNo = @custNo";

                    return SelectIntValueWithCustNo(commandText, custNo);
                }
            }
            return 0;
        }


        private int SelectIntValueWithCustNo(string command, int custNo)
        {
            var value = -1;

            _cmd = new SqlCommand(command, _connection);

          
            _cmd.Parameters.Add("@custNo", SqlDbType.Int).Value = custNo;

            try
            {
                using (var reader = _cmd.ExecuteReader())
              
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private string SelectStringValueWithProdNo(string command, string prodNo)
        {
            var value = string.Empty;

            _cmd = new SqlCommand(command, _connection);

    
            _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;

            try
            {
                using (var reader = _cmd.ExecuteReader())
  
                {
                    while (reader.Read())
                    {
                        value = Convert.ToString(reader[0]).Trim();
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int SelectIntValueWithProdNo(string command, string prodNo)
        {
            var value = -1;

            _cmd = new SqlCommand(command, _connection);


            _cmd.Parameters.Add("@prodNo", SqlDbType.VarChar).Value = prodNo;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                    
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int SelectIntValueWithOrderNo(string command, int orderNo)
        {
            var value = -1;

            _cmd = new SqlCommand(command, _connection);

       
            _cmd.Parameters.Add("@orderNo", SqlDbType.Int).Value = orderNo;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                   
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int SelectIntValueWithUser(string command)
        {
            var value = -1;

            _cmd = new SqlCommand(command, _connection);

            _cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = _username;

            try
            {
                using (var reader = _cmd.ExecuteReader())
                    
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private SqlCommand SelectQueryWithEmpNo(string selectCol)
        {
            var empNo = GetEmpno();

        
            var commandText = "Select " + selectCol + " From Actor " +
                              "Where empNo = @empNo";
            var cmd2 = new SqlCommand(commandText, _connection);

           
            cmd2.Parameters.Add("@empNo", SqlDbType.Int).Value = empNo;

            return cmd2;
        }


        private string GetExtid()
        {
            var value = string.Empty;
            _cmd = SelectQueryWithEmpNo("extid");

            try
            {
                using (var reader = _cmd.ExecuteReader())
                  
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToString(reader[0]);
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private double GetChext()
        {
            double value = -1;
            _cmd = SelectQueryWithEmpNo("chext");

            try
            {
                using (var reader = _cmd.ExecuteReader())
                    
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToDouble(reader[0]);
                    }
                    reader.Close(); 
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetEmpprgr()
        {
            var value = -1;
            _cmd = SelectQueryWithEmpNo("empprgr");

            try
            {
                using (var reader = _cmd.ExecuteReader())
                   
                {
                    while (reader.Read()) 
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetActtmzn()
        {
            var value = -1;
            _cmd = SelectQueryWithEmpNo("tmzn");

            try
            {
                using (var reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        value = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
            catch
            {
                // ignored
            }
            return value;
        }

        private int GetTransgr(int orderNo)
        {
            // Set up a command
            var commandText = "Select transgr From Ord " +
                              "Where ordNo = @orderNo";

            return SelectIntValueWithOrderNo(commandText, orderNo);
        }

        private float GetDc1Am()
        {
            return 0;
        }

        private float GetDc1Dam()
        {
            return 0;
        }


        private int GetDypri()
        {
            return 0;
        }

        private int GetAdwage2()
        {
            return 0;
        }

        private static int GetGmt()
        {
            return 0;
        }

        private int GetAgrtmzn()
        {
            return 0;
        }

        private int GetTsbfryrpr()
        {
            return 0;
        }

        private int GetTsbtoyrpr()
        {
            return 0;
        }

        private int GetTsproc()
        {
            return 0;
        }

        private int GetWgrunno()
        {
            return 0;
        }

        private int GetRsp()
        {
            return 0;
        }

        private int GetRsptp()
        {
            return 0;
        }

        private int GetPriv()
        {
            return 0;
        }

        private int GetRsvtm()
        {
            return 0;
        }

        private int GetRsvtmun()
        {
            return 0;
        }

        private int GetNtf()
        {
            return 0;
        }

        private int GetNtftm()
        {
            return 0;
        }

        private int GetNtftmun()
        {
            return 0;
        }

        private int GetStrst()
        {
            return 0;
        }

        private int GetChsm()
        {
            return 0;
        }

        private int GetDt1()
        {
            return 0;
        }

        private int GetDt2()
        {
            return 0;
        }

        private int GetTm1()
        {
            return 0;
        }

        private int GetTm2()
        {
            return 0;
        }

        private int GetGr()
        {
            return 0;
        }

        private int GetGr2()
        {
            return 0;
        }

        private int GetGr3()
        {
            return 0;
        }

        private int GetGr4()
        {
            return 0;
        }

        private int GetGr5()
        {
            return 0;
        }

        private int GetGr6()
        {
            return 0;
        }

        private int GetGr7()
        {
            return 0;
        }

        private int GetGr8()
        {
            return 0;
        }

        private static int GetGr9()
        {
            return 0;
        }

        private int GetGr10()
        {
            return 0;
        }

        private int GetGr11()
        {
            return 0;
        }

        private int GetGr12()
        {
            return 0;
        }

        private int GetExtproc()
        {
            return 0;
        }

        private int GetMaagract()
        {
            return 0;
        }

        private int GetMaagrno()
        {
            return 0;
        }

        private int GetResno()
        {
            return 0;
        }

        private int GetD1Hr()
        {
            return 0;
        }

        private int GetD2Hr()
        {
            return 0;
        }

        private int GetD3Hr()
        {
            return 0;
        }

        private int GetD4Hr()
        {
            return 0;
        }

        private int GetD5Hr()
        {
            return 0;
        }

        private static int GetD6Hr()
        {
            return 0;
        }

        private int GetD7Hr()
        {
            return 0;
        }

        private int GetStcno()
        {
            return 0;
        }

        private int GetWagertno()
        {
            return 0;
        }

        private int GetDebdy()
        {
            return 0;
        }

        private int GetPropno()
        {
            return 0;
        }

        private int GetTsgrno()
        {
            return 0;
        }

        private string GetTxt1()
        {
            return string.Empty;
        }

        private string GetTxt2()
        {
            return string.Empty;
        }

        
        public bool Insert(int custNo, int empNo, int ordNo, bool utlagg, string prodNo, bool debit, int contract,
            float workedTime,
            float faktureradTime, bool adWage, string descr, string descr2, bool defaultActivity, int frDt, int toDt,
            int frTm, int toTm,
            string service, string project, string activity, int taxa)
        {
            Mutex.WaitOne();

            _connection.Open();

            try
            {
                var custprg2 = taxa == -1 ? GetRightCustprgr("custprg2", custNo) : taxa;
                var creDate = GetCredt();
                var time = GetCurrentTime();
                var creTime = GetCretm(time);

                var commandText = "INSERT INTO Agr " +
                                  "(AgrActNo, AgrNo, FrDt, ToDt, FrTm, ToTm, NoReg, NoInvoAb, Pri, DyPri, ProdNo, ActNo, LiaActNo, " +
                                  "OrdNo, Descr, R1, R2, R3, R4, R5, R6, RspTp, Priv, RsvTm, RsvTmUn, Ntf, NtfTm, NtfTmUn, Fin, FinDt, " +
                                  "StrSt, Invo, NoteNm, Srt, ChDt, ChTm, ChUsr, LckSt, ChPrc, ChSm, CrActNo, Dt1, Tm1, Dt2, Tm2, PropNo, Gr," +
                                  "Gr2, Gr3, Gr4, Gr5, Gr6, Gr7, Gr8, Gr9, Gr10, Gr11, Gr12, Descr2, ExtID, ExtProc, ChExt, CustNo, InvoCust, Txt1, " +
                                  "Txt2, TransGr, MaAgrAct, MaAgrNo, ResNo, R7, R8, R9, R10, R11, R12, RefNo, AcYrPr, CCstPr, CIncCst, DPr, DAm, " +
                                  "D1Hr, D2Hr, D3Hr, D4Hr, D5Hr, D6Hr, D7Hr, OrdLnNo, Price, Cur, ExRt, AgrProc, CstPr, IncCst, WageSrt, ProdPrGr, " +
                                  "ProdPrG2, ProdPrG3, CustPrGr, CustPrG2, CustPrG3, StcNo, EmpNo, Un, PrM1, PrM2, PrM3, PrM4, PrM5, Dc1P, Dupl, Dup2, " +
                                  "Am, WageRtNo, EmpPrGr, Dc1Am, Dc1DAm, AdWage1, AdWage2, GMT, AgrTmZn, ActTmZn, CreUsr, CreDt, CreTm, TSGrNo, " +
                                  "TSBFrYrPr, TSBToYrPr, TSProc, CAcSet, WgRunNo, DebDy, NoRegDy, NoInvoDy, Rsp, ChTmms, ProdProcNo)" +
                                  " " +
                                  "VALUES(@AgrActNo, @AgrNo, @FrDt, @ToDt, @FrTm, @ToTm, @NoReg, @NoInvoAb, @Pri, @DyPri, @ProdNo, @ActNo, @LiaActNo, " +
                                  "@OrdNo, @Descr, @R1, @R2, @R3, @R4, @R5, @R6, @RspTp, @Priv, @RsvTm, @RsvTmUn, @Ntf, @NtfTm, @NtfTmUn, @Fin, @FinDt, " +
                                  "@StrSt, @Invo, @NoteNm, @Srt, @ChDt, @ChTm, @ChUsr, @LckSt, @ChPrc, @ChSm, @CrActNo, @Dt1, @Tm1, @Dt2, @Tm2, @PropNo, @Gr," +
                                  "@Gr2, @Gr3, @Gr4, @Gr5, @Gr6, @Gr7, @Gr8, @Gr9, @Gr10, @Gr11, @Gr12, @Descr2, @ExtID, @ExtProc, @ChExt, @CustNo, @InvoCust, @Txt1, " +
                                  "@Txt2, @TransGr, @MaAgrAct, @MaAgrNo, @ResNo, @R7, @R8, @R9, @R10, @R11, @R12, @RefNo, @AcYrPr, @CCstPr, @CIncCst, @DPr, @DAm, " +
                                  "@D1Hr, @D2Hr, @D3Hr, @D4Hr, @D5Hr, @D6Hr, @D7Hr, @OrdLnNo, @Price, @Cur, @ExRt, @AgrProc, @CstPr, @IncCst, @WageSrt, @ProdPrGr, " +
                                  "@ProdPrG2, @ProdPrG3, @CustPrGr, @CustPrG2, @CustPrG3, @StcNo, @EmpNo, @Un, @PrM1, @PrM2, @PrM3, @PrM4, @PrM5, @Dc1P, @Dupl, @Dup2, " +
                                  "@Am, @WageRtNo, @EmpPrGr, @Dc1Am, @Dc1DAm, @AdWage1, @AdWage2, @GMT, @AgrTmZn, @ActTmZn, @CreUsr, @CreDt, @CreTm, @TSGrNo, " +
                                  "@TSBFrYrPr, @TSBToYrPr, @TSProc, @CAcSet, @WgRunNo, @DebDy, @NoRegDy, @NoInvoDy, @Rsp, @ChTmms, @ProdProcNo)";

                var cmd2 = new SqlCommand(commandText, _connection);


                workedTime = GetNoregdy(workedTime);
                faktureradTime = GetNoregdy(faktureradTime);

                cmd2.Parameters.Add("@AgrActNo", SqlDbType.Int).Value = GetAgractno();
                cmd2.Parameters.Add("@AgrNo", SqlDbType.Int).Value = GetAgrno();
                cmd2.Parameters.Add("@FrDt", SqlDbType.Int).Value = frDt;
                cmd2.Parameters.Add("@ToDt", SqlDbType.Int).Value = toDt;
                cmd2.Parameters.Add("@FrTm", SqlDbType.SmallInt).Value = frTm;
                cmd2.Parameters.Add("@ToTm", SqlDbType.SmallInt).Value = toTm;
                cmd2.Parameters.Add("@NoReg", SqlDbType.Decimal).Value = workedTime; 
                cmd2.Parameters.Add("@NoInvoAb", SqlDbType.Decimal).Value = faktureradTime;
                cmd2.Parameters.Add("@Pri", SqlDbType.Int).Value = GetPri(defaultActivity);
                cmd2.Parameters.Add("@DyPri", SqlDbType.Int).Value = GetDypri();
                cmd2.Parameters.Add("@ProdNo", SqlDbType.VarChar).Value = prodNo;
                cmd2.Parameters.Add("@ActNo", SqlDbType.Int).Value = GetActno(custNo);
                cmd2.Parameters.Add("@LiaActNo", SqlDbType.Int).Value = GetLiaactno(ordNo);

                cmd2.Parameters.Add("@OrdNo", SqlDbType.Int).Value = ordNo;
                cmd2.Parameters.Add("@Descr", SqlDbType.VarChar).Value = descr;
                cmd2.Parameters.Add("@R1", SqlDbType.Int).Value = GetRightRInt("r1", contract);
                cmd2.Parameters.Add("@R2", SqlDbType.Int).Value = GetRightRInt("r2", contract);
                cmd2.Parameters.Add("@R3", SqlDbType.Int).Value = GetRightRInt("r3", contract);
                cmd2.Parameters.Add("@R4", SqlDbType.Int).Value = GetRightRInt("r4", contract);
                cmd2.Parameters.Add("@R5", SqlDbType.Int).Value = GetRightRInt("r5", contract);
                cmd2.Parameters.Add("@R6", SqlDbType.Int).Value = GetRightRInt("r6", contract);
                cmd2.Parameters.Add("@RspTp", SqlDbType.Int).Value = GetRsptp();
                cmd2.Parameters.Add("@Priv", SqlDbType.TinyInt).Value = GetPriv();
                cmd2.Parameters.Add("@RsvTm", SqlDbType.SmallInt).Value = GetRsvtm();
                cmd2.Parameters.Add("@RsvTmUn", SqlDbType.TinyInt).Value = GetRsvtmun();
                cmd2.Parameters.Add("@Ntf", SqlDbType.TinyInt).Value = GetNtf();
                cmd2.Parameters.Add("@NtfTm", SqlDbType.Int).Value = GetNtftm();
                cmd2.Parameters.Add("@NtfTmUn", SqlDbType.TinyInt).Value = GetNtftmun();
                cmd2.Parameters.Add("@Fin", SqlDbType.TinyInt).Value = GetFin();
                cmd2.Parameters.Add("@FinDt", SqlDbType.Int).Value = toDt;

                cmd2.Parameters.Add("@StrSt", SqlDbType.TinyInt).Value = GetStrst();
                cmd2.Parameters.Add("@Invo", SqlDbType.Int).Value = GetInvo(utlagg);
                cmd2.Parameters.Add("@NoteNm", SqlDbType.VarChar).Value = string.Empty;
                cmd2.Parameters.Add("@Srt", SqlDbType.Int).Value = GetSrt();
                cmd2.Parameters.Add("@ChDt", SqlDbType.Int).Value = creDate;
                cmd2.Parameters.Add("@ChTm", SqlDbType.SmallInt).Value = creTime;
                cmd2.Parameters.Add("@ChUsr", SqlDbType.VarChar).Value = GetChusr();
                cmd2.Parameters.Add("@LckSt", SqlDbType.TinyInt).Value = GetLckst();
                cmd2.Parameters.Add("@ChPrc", SqlDbType.Int).Value = GetChprc();
                cmd2.Parameters.Add("@ChSm", SqlDbType.Int).Value = GetChsm();
                cmd2.Parameters.Add("@CrActNo", SqlDbType.Int).Value = GetCractno();
                cmd2.Parameters.Add("@Dt1", SqlDbType.Int).Value = GetDt1();
                cmd2.Parameters.Add("@Tm1", SqlDbType.SmallInt).Value = GetTm1();
                cmd2.Parameters.Add("@Dt2", SqlDbType.Int).Value = GetDt2();
                cmd2.Parameters.Add("@Tm2", SqlDbType.SmallInt).Value = GetTm2();
                cmd2.Parameters.Add("@PropNo", SqlDbType.Int).Value = GetPropno();
                cmd2.Parameters.Add("@Gr", SqlDbType.Int).Value = GetGr();

                cmd2.Parameters.Add("@Gr2", SqlDbType.Int).Value = GetGr2();
                cmd2.Parameters.Add("@Gr3", SqlDbType.Int).Value = GetGr3();
                cmd2.Parameters.Add("@Gr4", SqlDbType.Int).Value = GetGr4();
                cmd2.Parameters.Add("@Gr5", SqlDbType.Int).Value = GetGr5();
                cmd2.Parameters.Add("@Gr6", SqlDbType.Int).Value = GetGr6();
                cmd2.Parameters.Add("@Gr7", SqlDbType.Int).Value = GetGr7();
                cmd2.Parameters.Add("@Gr8", SqlDbType.Int).Value = GetGr8();
                cmd2.Parameters.Add("@Gr9", SqlDbType.Int).Value = GetGr9();
                cmd2.Parameters.Add("@Gr10", SqlDbType.Int).Value = GetGr10();
                cmd2.Parameters.Add("@Gr11", SqlDbType.Int).Value = GetGr11();
                cmd2.Parameters.Add("@Gr12", SqlDbType.Int).Value = GetGr12();
                cmd2.Parameters.Add("@Descr2", SqlDbType.VarChar).Value = descr2;
                cmd2.Parameters.Add("@ExtID", SqlDbType.VarChar).Value = GetExtid();
                cmd2.Parameters.Add("@ExtProc", SqlDbType.TinyInt).Value = GetExtproc();
                cmd2.Parameters.Add("@ChExt", SqlDbType.Decimal).Value = GetChext();
                cmd2.Parameters.Add("@CustNo", SqlDbType.Int).Value = custNo;
                cmd2.Parameters.Add("@InvoCust", SqlDbType.Int).Value = GetInvocust(ordNo);
                cmd2.Parameters.Add("@Txt1", SqlDbType.VarChar).Value = GetTxt1();

                cmd2.Parameters.Add("@Txt2", SqlDbType.VarChar).Value = GetTxt2();
                cmd2.Parameters.Add("@TransGr", SqlDbType.Int).Value = GetTransgr(ordNo);
                cmd2.Parameters.Add("@MaAgrAct", SqlDbType.Int).Value = GetMaagract();
                cmd2.Parameters.Add("@MaAgrNo", SqlDbType.Int).Value = GetMaagrno();
                cmd2.Parameters.Add("@ResNo", SqlDbType.Int).Value = GetResno();
                cmd2.Parameters.Add("@R7", SqlDbType.VarChar).Value = GetRightRString("r7", service, project);
                cmd2.Parameters.Add("@R8", SqlDbType.VarChar).Value = GetRightRString("r8", service, project);
                cmd2.Parameters.Add("@R9", SqlDbType.VarChar).Value = GetRightRString("r9", service, project);
                cmd2.Parameters.Add("@R10", SqlDbType.VarChar).Value = GetRightRString("r10", service, project);
                cmd2.Parameters.Add("@R11", SqlDbType.VarChar).Value = GetRightRString("r11", service, project);
                cmd2.Parameters.Add("@R12", SqlDbType.VarChar).Value = GetRightRString("r12", service, project);
                cmd2.Parameters.Add("@RefNo", SqlDbType.Int).Value = GetRefno();
                cmd2.Parameters.Add("@AcYrPr", SqlDbType.Int).Value = GetAcyrpr(frDt);
                cmd2.Parameters.Add("@CCstPr", SqlDbType.Decimal).Value = GetCcstpr(prodNo, debit);
                cmd2.Parameters.Add("@CIncCst", SqlDbType.Decimal).Value = GetCinccst(prodNo, debit, workedTime);
                cmd2.Parameters.Add("@DPr", SqlDbType.Decimal).Value = GetDpr(custNo, prodNo, debit, contract,
                    faktureradTime, custprg2, ordNo);
                cmd2.Parameters.Add("@DAm", SqlDbType.Decimal).Value = GetDam(custNo, prodNo, debit, contract,
                    faktureradTime, custprg2, ordNo);

                cmd2.Parameters.Add("@D1Hr", SqlDbType.Int).Value = GetD1Hr();
                cmd2.Parameters.Add("@D2Hr", SqlDbType.Int).Value = GetD2Hr();
                cmd2.Parameters.Add("@D3Hr", SqlDbType.Int).Value = GetD3Hr();
                cmd2.Parameters.Add("@D4Hr", SqlDbType.Int).Value = GetD4Hr();
                cmd2.Parameters.Add("@D5Hr", SqlDbType.Int).Value = GetD5Hr();
                cmd2.Parameters.Add("@D6Hr", SqlDbType.Int).Value = GetD6Hr();
                cmd2.Parameters.Add("@D7Hr", SqlDbType.Int).Value = GetD7Hr();
                cmd2.Parameters.Add("@OrdLnNo", SqlDbType.Int).Value = GetOrdlnno();
                cmd2.Parameters.Add("@Price", SqlDbType.Decimal).Value = GetPrice(custNo, prodNo, debit, contract,
                    faktureradTime, custprg2, ordNo);
                cmd2.Parameters.Add("@Cur", SqlDbType.Int).Value = GetCur(ordNo);
                cmd2.Parameters.Add("@ExRt", SqlDbType.Decimal).Value = GetExrt(ordNo);
                cmd2.Parameters.Add("@AgrProc", SqlDbType.Int).Value = GetAgrproc(prodNo);
                cmd2.Parameters.Add("@CstPr", SqlDbType.Decimal).Value = GetCstpr(prodNo, debit);
                cmd2.Parameters.Add("@IncCst", SqlDbType.Decimal).Value = GetInccst(prodNo, debit, workedTime);
                cmd2.Parameters.Add("@WageSrt", SqlDbType.VarChar).Value = GetWagesrt(prodNo);
                cmd2.Parameters.Add("@ProdPrGr", SqlDbType.Int).Value = GetRightProdprgr("prodprgr", debit, utlagg,
                    activity);

                var custprgr = GetRightCustprgr("custprgr", custNo);
                cmd2.Parameters.Add("@ProdPrG2", SqlDbType.Int).Value = GetRightProdprgr("prodprg2", debit, utlagg,
                    activity);
                cmd2.Parameters.Add("@ProdPrG3", SqlDbType.Int).Value = GetRightProdprgr("prodprg3", debit, utlagg,
                    activity);
                cmd2.Parameters.Add("@CustPrGr", SqlDbType.Int).Value = custprgr;
                cmd2.Parameters.Add("@CustPrG2", SqlDbType.Int).Value = GetRightCustprgr("custprg2", custNo);
                cmd2.Parameters.Add("@CustPrG3", SqlDbType.Int).Value = GetRightCustprgr("custprg3", custNo);
                cmd2.Parameters.Add("@StcNo", SqlDbType.Int).Value = GetStcno();
                cmd2.Parameters.Add("@EmpNo", SqlDbType.Int).Value = empNo;
                cmd2.Parameters.Add("@Un", SqlDbType.Int).Value = GetUn(prodNo);
                cmd2.Parameters.Add("@PrM1", SqlDbType.Int).Value = GetPrm1(prodNo);
                cmd2.Parameters.Add("@PrM2", SqlDbType.Int).Value = GetPrm2(prodNo);
                cmd2.Parameters.Add("@PrM3", SqlDbType.Int).Value = GetPrm3(prodNo);
                cmd2.Parameters.Add("@PrM4", SqlDbType.Int).Value = GetPrm4(prodNo);
                cmd2.Parameters.Add("@PrM5", SqlDbType.Int).Value = GetPrm5(prodNo);
                cmd2.Parameters.Add("@Dc1P", SqlDbType.Decimal).Value = GetDc1P(prodNo, debit, custprgr, custNo,
                    contract);
                cmd2.Parameters.Add("@Dupl", SqlDbType.Int).Value = GetDupl();
                cmd2.Parameters.Add("@Dup2", SqlDbType.Int).Value = GetDup2();

                cmd2.Parameters.Add("@Am", SqlDbType.Decimal).Value = GetAm(custNo, prodNo, debit, contract,
                    faktureradTime, true, custprg2, ordNo);
                cmd2.Parameters.Add("@WageRtNo", SqlDbType.Int).Value = GetWagertno();
                cmd2.Parameters.Add("@EmpPrGr", SqlDbType.Int).Value = GetEmpprgr();
                cmd2.Parameters.Add("@Dc1Am", SqlDbType.Decimal).Value = GetDc1Am();
                cmd2.Parameters.Add("@Dc1DAm", SqlDbType.Decimal).Value = GetDc1Dam();
                cmd2.Parameters.Add("@AdWage1", SqlDbType.Int).Value = GetAdwage1(adWage);
                cmd2.Parameters.Add("@AdWage2", SqlDbType.Int).Value = GetAdwage2();
                cmd2.Parameters.Add("@GMT", SqlDbType.SmallInt).Value = GetGmt();
                cmd2.Parameters.Add("@AgrTmZn", SqlDbType.Int).Value = GetAgrtmzn();
                cmd2.Parameters.Add("@ActTmZn", SqlDbType.Int).Value = GetActtmzn();
                cmd2.Parameters.Add("@CreUsr", SqlDbType.VarChar).Value = GetCreusr();
                cmd2.Parameters.Add("@CreDt", SqlDbType.Int).Value = creDate;
                cmd2.Parameters.Add("@CreTm", SqlDbType.SmallInt).Value = creTime;
                cmd2.Parameters.Add("@TSGrNo", SqlDbType.Int).Value = GetTsgrno();

                cmd2.Parameters.Add("@TSBFrYrPr", SqlDbType.Int).Value = GetTsbfryrpr();
                cmd2.Parameters.Add("@TSBToYrPr", SqlDbType.Int).Value = GetTsbtoyrpr();
                cmd2.Parameters.Add("@TSProc", SqlDbType.Int).Value = GetTsproc();
                cmd2.Parameters.Add("@CAcSet", SqlDbType.Int).Value = GetCacset(ordNo);
                cmd2.Parameters.Add("@WgRunNo", SqlDbType.Int).Value = GetWgrunno();
                cmd2.Parameters.Add("@DebDy", SqlDbType.Int).Value = GetDebdy();
                cmd2.Parameters.Add("@NoRegDy", SqlDbType.Decimal).Value = workedTime;
                cmd2.Parameters.Add("@NoInvoDy", SqlDbType.Decimal).Value = faktureradTime;
                cmd2.Parameters.Add("@Rsp", SqlDbType.Int).Value = GetRsp();
                cmd2.Parameters.Add("@ChTmms", SqlDbType.Int).Value = GetChtmms(time);
                cmd2.Parameters.Add("@ProdProcNo", SqlDbType.Int).Value = 0;


                try
                {
                    var rowEffected = cmd2.ExecuteNonQuery();
                    if (rowEffected > 0)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    // ignored
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                Mutex.ReleaseMutex();
                _connection.Close();
            }
            return false;
        }
    }
}