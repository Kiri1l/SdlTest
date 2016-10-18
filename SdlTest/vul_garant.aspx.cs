using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;

namespace SdlTest
{
    public partial class vul_garant : System.Web.UI.Page
    {
        public static class UnknownType
        {
            public static string Property1;
            public static string Property2;
            public static string Property3;
        }

        public string ClearXSS(string nonClear)
        {
            return nonClear != null ? nonClear.Replace("(","").Replace(")", "").Replace("<", "").Replace(">", "") : "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var parm1 = Request.Params["parm1"];
            const string cond1 = "ZmFsc2U="; // "false" в base64-кодировке
            Action<string> pvo = Response.Write;

            // False-negative
            // Анализаторы, не интерпретирующие поток выполнения по потокам данных функционального типа, не сообщат здесь об уязвимости
            pvo(parm1);

            // Для анализаторов, требующих компилируемый код, этот фрагмент необходимо удалить
            #region

            var argument = "harmless value";

            UnknownType.Property1 = parm1;
            UnknownType.Property2 = UnknownType.Property1;
            UnknownType.Property3 = cond1;

            if (UnknownType.Property3 == null)
            {
                argument = UnknownType.Property2;
            }

            // False-positive
            // Анализаторы, игнорирующие некомпилируемый код, сообщат здесь об уязвимости
            Response.Write(argument);

            #endregion

            // False-positive
            // Анализаторы, не учитывающие условия достижимости точек выполнения, сообщат здесь об уязвимости
            if (cond1 == null) {Response.Write(parm1); }

            // False-positive
            // Анализаторы, не учитывающие семантику стандартных фильтрующих функций, сообщат здесь об уязвимости
            Response.Write(WebUtility.HtmlEncode(parm1));

            // False-positive
            // Анализаторы, не учитывающие семантику нестандартных фильтрующих функций, сообщат здесь об уязвимости
            // (CustomFilter.Filter реализует логику `s.Replace("<", string.Empty).Replace(">", string.Empty)`)
            Response.Write(ClearXSS(parm1));

            if (Encoding.UTF8.GetString(Convert.FromBase64String(cond1)) == "true")
            {
                // False-positive
                // Анализаторы, не учитывающие семантику стандартных кодирующих функций, сообщат здесь об уязвимости
                Response.Write(parm1);
            }

            var sum = 0;
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    sum += i + j;
                }
            }
            if (sum != 1725)
            {
                // False-positive
                // Анализаторы, аппроксимирующие или игнорирующие интерпретацию циклов, сообщат здесь об уязвимости
                Response.Write(parm1);
            }

            var sb = new StringBuilder();
            sb.Append(cond1);
            if (sb.ToString() == "true")
            {
                // False-positive
                // Анализаторы, не интерпретирующие семантику типов стандартной библиотеки, сообщат здесь об уязвимости
                Response.Write(parm1);
            }
        }
    }
}