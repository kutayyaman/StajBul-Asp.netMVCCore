#pragma checksum "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "33b472e43785ae30973a39803dd28af94a9454d9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Details), @"mvc.1.0.view", @"/Views/Home/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\_ViewImports.cshtml"
using StajBul.WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\_ViewImports.cshtml"
using StajBul.WebUI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\_ViewImports.cshtml"
using StajBul.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33b472e43785ae30973a39803dd28af94a9454d9", @"/Views/Home/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d767e7ee0e27e70bbc57b7dd85d75e514fb6eb51", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<InternshipAnnouncement>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n\r\n            <div class=\"card text-center\">\r\n                <div class=\"card-header\">\r\n                    ");
#nullable restore
#line 11 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
               Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">");
#nullable restore
#line 14 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                      Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <small class=\"card-subtitle mb-2 text-muted\">");
#nullable restore
#line 15 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                            Write(Model.Category.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n                    <p class=\"card-text\">");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                    Write(Model.Explanation);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                             Write(Model.Address?.City.CityName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                                                                Write(Model.Address?.District);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                                                                                              Write(Model.Address?.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                                                                                                                                Write(Model.Address?.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                                                                                                                                                                  Write(Model.Address?.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 16 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
                                                                                                                                                                                                                                  Write(Model.Mail);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                    <a href=\'javascript:history.go(-1)\' class=\"btn btn-primary\">Geri Git</a>\r\n                    <a class=\"btn btn-primary ml-2\"");
            BeginWriteAttribute("href", " href=\"", 870, "\"", 907, 2);
            WriteAttributeValue("", 877, "/User/Profile?id=", 877, 17, true);
#nullable restore
#line 18 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
WriteAttributeValue("", 894, Model.UserId, 894, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Kullanıcı Profilini Gör</a>\r\n                </div>\r\n                <div class=\"card-footer \">\r\n                    ");
#nullable restore
#line 21 "C:\Users\kutay\source\repos\StajBul\StajBul.WebUI\Views\Home\Details.cshtml"
               Write(Model.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<InternshipAnnouncement> Html { get; private set; }
    }
}
#pragma warning restore 1591