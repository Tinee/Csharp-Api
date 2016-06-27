using System;
using System.Configuration;


namespace Business_Logic.Config
{
    public class Config
    {
        #region Consts

   
        public string KeyAdwageFalse = "adwageFalse";
        public string KeyAdwageTrue = "adwageTrue";

        //DB
        public string KeyDbDatabaseName = "DBDatabaseName";
        public string KeyDbPassword = "DBPassword";
        public string KeyDbServer = "DBServer";
        public string KeyDbUserId = "DBUserId";
        public string KeyDebit = "Debit";
        public string KeyDebitEjCode = "debitEjCode";
        public string KeyDebiteraCode = "debiteraCode";
        public string KeyEjDebit = "EjDebit";
        public string KeyMemo = "memoPath";
        public string KeyNCacset = "normalCacsetActivity";
        public string KeyNInvo = "invoReg";
        public string KeyNPri = "normalPriActivity";
        public string KeyOCacset = "otherCacsetActivity";
        public string KeyOInvo = "invoUtl";
        public string KeyOPri = "otherPriActivity";
        public string KeyTaxa = "taxaCode";
        public string KeyactivityCode = "activityCode";
        public string KeycustPrG2 = "custprg2";
        public string KeycustPrG3 = "custprg3";
        public string KeycustPrGr = "custprgr";
        public string Keylang = "lang";

    

        public string KeyprodPrG2 = "prodprg2";
        public string KeyprodPrG3 = "prodprg3";
        public string KeyprodPrGr = "prodprgr";

        //If edit this line => change this function db.getRightRInt()
        public string Keyr1 = "r1";
        public string Keyr10 = "r10";
        public string Keyr11 = "r11";
        public string Keyr12 = "r12";
        public string Keyr2 = "r2";
        public string Keyr3 = "r3";
        public string Keyr4 = "r4";
        public string Keyr5 = "r5";
        public string Keyr6 = "r6";

        //If edit this line => change this function db.getRightRString()
        public string Keyr7 = "r7";
        public string Keyr8 = "r8";
        public string Keyr9 = "r9";
        public string KeysmtpPassword = "smtpPassword";
        public string KeysmtpPort = "smtpPort";
        public string KeysmtpSsl = "smtpSSL";
        public string KeysmtpServer = "smtpServer";
        public string KeysmtpUserName = "smtpUserName";
        public string KeyyearLifeOfTheCookie = "yearLifeOfTheCookie";
        public string[] ProdPrGrArr = { "Debit", "Utlagg", "Activity", string.Empty };
        public string[] RPrGrIntArr = { "Locale", "Contract", string.Empty };
        public string[] RPrGrStringArr = { "Service", "Project", string.Empty };
        public string VarDate = "%date%";
        public string VarDelim = "#?#";
        public string VarDynamic = "DYNC!";
        public string VarEmpty = string.Empty;
        public string VarIp = "%IPaddress%";
        public string VarOffert = "Offert";
        public string VarUtlagg = "Utlägg";
        public string VarWebNm = "%webbName%";

        #endregion

        #region Public properties

        /* Domain name */
        public string DomainAd { get; set; }

        /* Paths for the regular users */
        public string LoginPage { get; private set; }
        public string UserPage { get; private set; }
        public string PcUserPage { get; private set; }
        public string TimePage { get; private set; }

        /* Paths for the admin */
        public string AdminLoginPage { get; private set; }
        public string AdminPage { get; private set; }

        /* Path to the config-file */
        public string ConfigPath { get; private set; }

        public int LoginAttempt { get; set; }
        public int YearLifeOfTheCookie { get; set; }


        public int Lang { get; set; }

        /* DB - Info */
        public string DbServer { get; set; }
        public string DbName { get; set; }
        public string DbUser { get; set; }
        public string DbPass { get; set; }

        /* Mail - Info */
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string MailFrom { get; set; }
        public string MailUser { get; set; }
        public string MailPass { get; set; }
        public bool MailSsl { get; set; }

        /* Error text */
        public string ErrorTextNoConToAd { get; private set; }
        public string ErrorTextUserNotMember { get; private set; }
        public string ErrorTextFindNoUser { get; private set; }
        public string ErrorTextAccountDisabled { get; private set; }
        public string ErrorTextDbUserExistsNot { get; private set; }
        public string ErrorTextWrongUserPass { get; private set; }
        public string ErrorTextTempCred { get; private set; }
        public string ErrorTextNoConToDb { get; private set; }

        /* Member groups */
        public string MemberDefault { get; set; }
        public string MemberAdmin { get; set; }


        public int AdWTrue { get; private set; }
        public int AdWFalse { get; private set; }

        public int NPriAct { get; set; }
        public int OPriAct { get; set; }

        //Debit or not for activities
        public string DebiteraActivity { get; set; }
        public string DebitEjActivity { get; set; }

        //The different code if debit or not
        public int DebiteraCode { get; private set; }
        public int DebitEjCode { get; private set; }

        public int NCacset { get; set; }
        public int OCacset { get; set; }

        public int InvoUtl { get; set; }
        public int InvoReg { get; set; }

        public int Fin { get; private set; }

        public int ActivityId { get; set; }

        public string TempAdmin { get; private set; }
        public string TempPass { get; private set; }

        public int OrderFakturaId { get; set; }

        public string ProdPrGr { get; private set; }
        public string ProdPrG2 { get; private set; }
        public string ProdPrG3 { get; private set; }

        public string R1 { get; private set; }
        public string R2 { get; private set; }
        public string R3 { get; private set; }
        public string R4 { get; private set; }
        public string R5 { get; private set; }
        public string R6 { get; private set; }

        public string R7 { get; private set; }
        public string R8 { get; private set; }
        public string R9 { get; private set; }
        public string R10 { get; private set; }
        public string R11 { get; private set; }
        public string R12 { get; private set; }

        public string CustPrGr { get; private set; }
        public string CustPrG2 { get; private set; }
        public string CustPrG3 { get; private set; }

        public string InternOrder { get; private set; }

        public string MailText { get; private set; }

        public int TaxaId { get; set; }
        public string MemoPath { get; set; }

        #endregion

  
        public Config()
        {
            #region Sets variables

            /* Name of the domain */

            /* Users */
            LoginPage = ConfigurationManager.AppSettings["loginPage"];
            UserPage = ConfigurationManager.AppSettings["userPage"];
            PcUserPage = ConfigurationManager.AppSettings["pcUserPage"];
            TimePage = ConfigurationManager.AppSettings["timePage"];
            /* Admin */
            AdminLoginPage = ConfigurationManager.AppSettings["adminLoginPage"];
            AdminPage = ConfigurationManager.AppSettings["adminPage"];
            //Path to the config
            ConfigPath = ConfigurationManager.AppSettings["configPath"];
            //Paths to configFile-Nodes

            Lang = Convert.ToInt32(ConfigurationManager.AppSettings[Keylang]);
            DbServer = ConfigurationManager.AppSettings[KeyDbServer];
            DbName = ConfigurationManager.AppSettings[KeyDbDatabaseName];
            DbUser = ConfigurationManager.AppSettings[KeyDbUserId];
            DbPass = ConfigurationManager.AppSettings[KeyDbPassword];
            MailServer = ConfigurationManager.AppSettings[KeysmtpServer];
            MailPort = Convert.ToInt32(ConfigurationManager.AppSettings[KeysmtpPort]);
            MailUser = ConfigurationManager.AppSettings[KeysmtpUserName];
            MailPass = ConfigurationManager.AppSettings[KeysmtpPassword];
            MailSsl = Convert.ToBoolean(ConfigurationManager.AppSettings[KeysmtpSsl]);
            ErrorTextNoConToAd = ConfigurationManager.AppSettings["errorTextNoConToAD"];
            ErrorTextUserNotMember = ConfigurationManager.AppSettings["errorTextUserNotMember"];
            ErrorTextFindNoUser = ConfigurationManager.AppSettings["errorTextFindNoUser"];
            ErrorTextAccountDisabled = ConfigurationManager.AppSettings["errorTextAccountDisabled"];
            ErrorTextDbUserExistsNot = ConfigurationManager.AppSettings["errorTextDBUserExistsNot"];
            ErrorTextWrongUserPass = ConfigurationManager.AppSettings["errorTextWrongUserPass"];
            ErrorTextTempCred = ConfigurationManager.AppSettings["errorTextTempCred"];
            ErrorTextNoConToDb = ConfigurationManager.AppSettings["errorTextNoConToDB"];
            YearLifeOfTheCookie = Convert.ToInt32(ConfigurationManager.AppSettings[KeyyearLifeOfTheCookie]);      
            ActivityId = Convert.ToInt32(ConfigurationManager.AppSettings[KeyactivityCode]);
            DebiteraActivity = ConfigurationManager.AppSettings[KeyDebit + ActivityId];
            DebitEjActivity = ConfigurationManager.AppSettings[KeyEjDebit + ActivityId];
            DebiteraCode = Convert.ToInt32(ConfigurationManager.AppSettings[KeyDebiteraCode]);
            DebitEjCode = Convert.ToInt32(ConfigurationManager.AppSettings[KeyDebitEjCode]);
            AdWTrue = Convert.ToInt32(ConfigurationManager.AppSettings[KeyAdwageTrue]);
            AdWFalse = Convert.ToInt32(ConfigurationManager.AppSettings[KeyAdwageFalse]);
            NPriAct = Convert.ToInt32(ConfigurationManager.AppSettings[KeyNPri]);
            OPriAct = Convert.ToInt32(ConfigurationManager.AppSettings[KeyOPri]);
            NCacset = Convert.ToInt32(ConfigurationManager.AppSettings[KeyNCacset]);
            OCacset = Convert.ToInt32(ConfigurationManager.AppSettings[KeyOCacset]);
            InvoUtl = Convert.ToInt32(ConfigurationManager.AppSettings[KeyOInvo]);
            InvoReg = Convert.ToInt32(ConfigurationManager.AppSettings[KeyNInvo]); 
            ProdPrGr = ConfigurationManager.AppSettings[KeyprodPrGr];
            ProdPrG2 = ConfigurationManager.AppSettings[KeyprodPrG2];
            ProdPrG3 = ConfigurationManager.AppSettings[KeyprodPrG3];
            R1 = ConfigurationManager.AppSettings[Keyr1];
            R2 = ConfigurationManager.AppSettings[Keyr2];
            R3 = ConfigurationManager.AppSettings[Keyr3];
            R4 = ConfigurationManager.AppSettings[Keyr4];
            R5 = ConfigurationManager.AppSettings[Keyr5];
            R6 = ConfigurationManager.AppSettings[Keyr6];
            R7 = ConfigurationManager.AppSettings[Keyr7];
            R8 = ConfigurationManager.AppSettings[Keyr8];
            R9 = ConfigurationManager.AppSettings[Keyr9];
            R10 = ConfigurationManager.AppSettings[Keyr10];
            R11 = ConfigurationManager.AppSettings[Keyr11];
            R12 = ConfigurationManager.AppSettings[Keyr12];
            CustPrGr = ConfigurationManager.AppSettings[KeycustPrGr];
            CustPrG2 = ConfigurationManager.AppSettings[KeycustPrG2];
            CustPrG3 = ConfigurationManager.AppSettings[KeycustPrG3];
   
            TaxaId = Convert.ToInt32(ConfigurationManager.AppSettings[KeyTaxa]);
            MemoPath = ConfigurationManager.AppSettings[KeyMemo];

            #endregion
        }

      

        protected string GetRType(string rGroup)
        {
            return ConfigurationManager.AppSettings[rGroup];
        }

        /// <summary>
        ///     Gets the Prod-Type forom the configFile
        /// </summary>
        /// <param name="prodGroup">Which prod-Group took look after</param>
        /// <returns>Returns a string with the value</returns>
        protected string GetProdPrType(string prodGroup)
        {
            return ConfigurationManager.AppSettings.Get(prodGroup);
        }

        /// <summary>
        ///     Gets the prods for a specific activity
        /// </summary>
        /// <param name="activity">The specific activity, to get the prods for</param>
        /// <returns>Returns a sting array with the prodCodes</returns>
        protected string[] GetProdCodeList(string activity)
        {
            string str = ConfigurationManager.AppSettings[activity];
            return !string.IsNullOrEmpty(str) ? str.Split(',') : new string[0];
        }

        /// <summary>
        ///     Gets the customer-PriceType from the configFile
        /// </summary>
        /// <param name="custGroup">Which customer-Group to look after</param>
        /// <returns>Returns a string with the value</returns>
        protected string GetCustPrType(string custGroup)
        {
            return ConfigurationManager.AppSettings[custGroup];
        } 
    }
}