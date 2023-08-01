using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JBHFoundation;

namespace YouDriveAuto.Classes
{
    public class LoanJournal
    {
        public bool ErrorFlag { get; set; } = false;
        public string Message { get; set; } = "Ok";

        public List<LoanJournalObject> LoanJournalItems = new List<LoanJournalObject>();
        public List<PickListItem> SearchOptions = new List<PickListItem>();

        public void LoadLoanJournal(string U_ID, string Loan_ID, string Keyword, string SortField, string SortAorD, string PageNo, string User, string Activity)
        {
            this.Message = "Ok";
            this.ErrorFlag = false;
            string T = "";
            AppCommon AC = new AppCommon();

            // Get our data
            SqlData D = new SqlData();
            string strSql = "EXEC GetLoanJournalFull @U_ID, @Loan_ID, @Keyword, @SortField, @SortAorD, @PageNo, @User, @Activity ";
            Dictionary<string, string> sqlParams = new Dictionary<string, string>();
            sqlParams.Add("@U_ID", U_ID);
            sqlParams.Add("@Loan_ID", Loan_ID);
            sqlParams.Add("@Keyword", Keyword);
            sqlParams.Add("@SortField", SortField);
            sqlParams.Add("@SortAorD", SortAorD);
            sqlParams.Add("@PageNo", PageNo);
            sqlParams.Add("@User", User);
            sqlParams.Add("@Activity", Activity);

            DataSet ds = D.GetDataSet(strSql, sqlParams);
            if (D.ErrorFlag)
            {
                this.ErrorFlag = true;
                this.Message = "ERROR GetLoanJournalFull: " + D.Message;
                return;
            }
            // Popluate LoanJournalItems
            LoanJournalObject ljo = null;
            foreach (DataRow drLJ in ds.Tables[0].Rows)
            {
                ljo = new LoanJournalObject();
                ljo.MaxRows = (int)drLJ["MaxRows"];
                ljo.Permissions = drLJ["Permissions"].ToString();
                ljo.LJ_ID = drLJ["LJ_ID"].ToString();
                ljo.Entry_Date = drLJ["EntryDate"].ToString();
                T = drLJ["PhoneNumber"].ToString();
                if (T.Length > 8)
                {
                    T = AC.FormatMyPhoneNumber(T);
                }
                ljo.PhoneNumber = T;
                ljo.Agent = drLJ["Agent"].ToString();
                ljo.Activity = drLJ["Activity"].ToString();
                ljo.Comment = drLJ["Comment"].ToString();
                ljo.IsHidden = drLJ["IsHidden"].ToString();
                ljo.Abs_Con_ID = drLJ["Abs_Con_ID"].ToString();
                ljo.CallRecordingsCount = drLJ["CallRecordingsCount"].ToString();
                ljo.SentToRepros = drLJ["SentToRepros"].ToString();
                ljo.CallID = drLJ["CallID"].ToString();
                ljo.CallID2 = drLJ["CallID2"].ToString();
                LoanJournalItems.Add(ljo);
            }
            // SearchOptions option
            PickListItem pli = null;
            foreach (DataRow drLJ in ds.Tables[1].Rows)
            {
                pli = new PickListItem();
                pli.Parameter1 = drLJ["Parameter1"].ToString();
                pli.ListItem = drLJ["ListItem"].ToString();
                pli.ListItemValue = drLJ["ListItemValue"].ToString();
                SearchOptions.Add(pli);
            }
            ds = null;
            D = null;
        }
    }

    public class LoanJournalObject
    {
        public int MaxRows
        { get; set; }
        public string Permissions
        { get; set; }
        public string LJ_ID
        { get; set; }
        public string Entry_Date
        { get; set; }
        public string PhoneNumber
        { get; set; }
        public string Agent
        { get; set; }
        public string Activity
        { get; set; }
        public string Comment
        { get; set; }
        public string IsHidden
        { get; set; }
        public string Abs_Con_ID
        { get; set; }
        public string CallRecordingsCount
        { get; set; }
        public string SentToRepros
        { get; set; }
        public string CallID
        { get; set; }
        public string CallID2
        { get; set; }
    }
}