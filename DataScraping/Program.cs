using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            DataScraper ds = new DataScraper();

            string url = "https://www.datahen.com/blog/data-scraping-vs-data-crawling/";
            string url2 = "https://sedar.com/FindCompanyDocuments.do?lang=EN&page_no=1&company_search=All+(or+type+a+name)&document_selection=10&industry_group=A&FromDate=1&FromMonth=1&FromYear=2020&ToDate=1&ToMonth=4&ToYear=2020&Variable=Issuer";

            //ds.WriteHtmlToFile(ds.getHTMLPage(url));
            //ds.WriteHtmlToFile(ds.getHTMLPageUsingPhantomJS(url2));
        }
    }
}
