using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace QuotableWeb
{
    /// <summary>
    /// Summary description for getInfo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class getInfo : System.Web.Services.WebService
    {

        public class categories
        {
            public int categoryID { get; set; }
            public string category { get; set; }

            public categories(int categoryID, string category)
            {
                this.categoryID = categoryID;
                this.category = category;
            }

        }

        public class authors
        {
            public int authorID { get; set; }
            public string author { get; set; }

            public authors(int authorID, string author)
            {
                this.authorID = authorID;
                this.author = author;
            }

        }

        public class quotes
        {
            public int quoteID { get; set; }
            public string quote { get; set; }
            public string author { get; set; }

            public quotes(int quoteID, string quote, string author)
            {
                this.quoteID = quoteID;
                this.quote = quote;
                this.author = author;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getCategories()
        {
            string jsonString = "";


            categories q = null;
            categories[] categoriesArray = null;
            // Type type = quotes.GetType();
            DataAccess myAccess = new DataAccess();
            DataSet myDS = myAccess.getQuery("spGetCategories");
            categoriesArray = new categories[myDS.Tables[0].Rows.Count];

            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {
                q = new categories(Int16.Parse(myDS.Tables[0].Rows[i]["categoryID"].ToString()), myDS.Tables[0].Rows[i]["category"].ToString());
                categoriesArray[i] = q;
                // using a third party component to create a json object
                //jsonString += Newtonsoft.Json.JsonConvert.SerializeObject(q);

            }
            jsonString = "{\"cat\":" + Newtonsoft.Json.JsonConvert.SerializeObject(categoriesArray) + "}";
            return jsonString;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getQuotesByCategory(int categoryID)
        {
            string jsonString = "";


            quotes q = null;

            // Type type = quotes.GetType();
            DataAccess myAccess = new DataAccess();
            SqlParameter[] myParameter = new SqlParameter[1];
            myParameter[0] = new SqlParameter("@categoryID", categoryID);


            DataSet myDS = myAccess.getQuery("spGetQuotesByCategory", myParameter);

            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {

                q = new quotes(Int16.Parse(myDS.Tables[0].Rows[i]["quoteID"].ToString()), myDS.Tables[0].Rows[i]["quote"].ToString(), myDS.Tables[0].Rows[i]["author"].ToString());
                // using a third party component to create a json object
                jsonString += Newtonsoft.Json.JsonConvert.SerializeObject(q);

            }
            return jsonString;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getAuthors()
        {
            string jsonString = "";


            authors a = null;
            authors[] authorArray = null;
            // Type type = quotes.GetType();
            DataAccess myAccess = new DataAccess();
            DataSet myDS = myAccess.getQuery("spGetAuthors");
            authorArray = new authors[myDS.Tables[0].Rows.Count];

            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {
                a = new authors(Int16.Parse(myDS.Tables[0].Rows[i]["authorID"].ToString()), myDS.Tables[0].Rows[i]["author"].ToString());
                authorArray[i] = a;
                // using a third party component to create a json object
                //jsonString += Newtonsoft.Json.JsonConvert.SerializeObject(q);

            }
            jsonString = "{\"cat\":" + Newtonsoft.Json.JsonConvert.SerializeObject(authorArray) + "}";
            return jsonString;
        }

    }
}
