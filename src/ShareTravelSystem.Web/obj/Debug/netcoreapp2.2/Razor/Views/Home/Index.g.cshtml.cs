#pragma checksum "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e140b141c9194ab18a025d94e43eed027cdb0027"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\_ViewImports.cshtml"
using ShareTravelSystem.Web;

#line default
#line hidden
#line 2 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\_ViewImports.cshtml"
using ShareTravelSystem.Web.Models;

#line default
#line hidden
#line 2 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 3 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
using ShareTravelSystem.Web.Areas.Identity.Data;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e140b141c9194ab18a025d94e43eed027cdb0027", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"792c70cac02df858ab894ff362fe29d15ae361bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShareTravelSystem.ViewModels.DisplayAllAnnouncementsViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(320, 77, true);
            WriteLiteral("<!-- Main jumbotron for a primary marketing message or call to action -->\r\n\r\n");
            EndContext();
#line 11 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
 if (SignInManager.IsSignedIn(User))
{

#line default
#line hidden
            BeginContext(438, 645, true);
            WriteLiteral(@"    <div class=""jumbotron"">
        <div class=""container"">
            <h1 class=""display-3"">Hello, world!</h1>
            <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
            <p><a class=""btn btn-primary btn-lg"" href=""#"" role=""button"">Learn more &raquo;</a></p>
        </div>
    </div>
    <div class=""container"">
        <h4 class=""mt-0"">Latest news</h4>
        <hr />
        <!-- Example row of columns -->
        <div class=""card-deck row"">
");
            EndContext();
#line 25 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
             foreach (var announcement in @Model.Announcements)
            {

#line default
#line hidden
            BeginContext(1163, 156, true);
            WriteLiteral("                <div class=\"card bg-secondary col-3\">\r\n                    <div class=\"card-body\">\r\n                        <h5 class=\"card-title \"><strong>");
            EndContext();
            BeginContext(1320, 18, false);
#line 29 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
                                                   Write(announcement.Title);

#line default
#line hidden
            EndContext();
            BeginContext(1338, 62, true);
            WriteLiteral("</strong></h5>\r\n                        <p class=\"card-text \">");
            EndContext();
            BeginContext(1401, 20, false);
#line 30 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
                                         Write(announcement.Content);

#line default
#line hidden
            EndContext();
            BeginContext(1421, 151, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                    <div class=\"card-footer row\">\r\n                        <small class=\"text-muted\">Last updated on ");
            EndContext();
            BeginContext(1573, 23, false);
#line 33 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
                                                             Write(announcement.CreateDate);

#line default
#line hidden
            EndContext();
            BeginContext(1596, 4, true);
            WriteLiteral(" by ");
            EndContext();
            BeginContext(1601, 19, false);
#line 33 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
                                                                                         Write(announcement.Author);

#line default
#line hidden
            EndContext();
            BeginContext(1620, 62, true);
            WriteLiteral("</small>\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 36 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(1697, 62, true);
            WriteLiteral("        </div>\r\n        <hr>\r\n    </div> <!-- /container -->\r\n");
            EndContext();
#line 40 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(1771, 479, true);
            WriteLiteral(@"    <div class=""jumbotron"">
        <div class=""container"">
            <h1 class=""display-3"">Hello, world!</h1>
            <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
            <p><a class=""btn btn-primary btn-lg"" href=""#"" role=""button"">Learn more &raquo;</a></p>
        </div>
    </div>
");
            EndContext();
#line 50 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Home\Index.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<ShareTravelSystemUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<ShareTravelSystemUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShareTravelSystem.ViewModels.DisplayAllAnnouncementsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
