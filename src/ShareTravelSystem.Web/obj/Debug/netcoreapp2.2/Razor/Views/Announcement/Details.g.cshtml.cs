#pragma checksum "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b4c46547f7e04d09de43717e098b224f9176e01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Announcement_Details), @"mvc.1.0.view", @"/Views/Announcement/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Announcement/Details.cshtml", typeof(AspNetCore.Views_Announcement_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b4c46547f7e04d09de43717e098b224f9176e01", @"/Views/Announcement/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"792c70cac02df858ab894ff362fe29d15ae361bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Announcement_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShareTravelSystem.ViewModels.Announcement.DetailsAnnouncementViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Announcement", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn chushka-bg-color"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(122, 68, true);
            WriteLiteral("\r\n\r\n<!-- Material form register -->\r\n\r\n<h1 class=\"text-center\">Id : ");
            EndContext();
            BeginContext(191, 8, false);
#line 9 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                        Write(Model.Id);

#line default
#line hidden
            EndContext();
            BeginContext(199, 156, true);
            WriteLiteral("</h1>\r\n<hr class=\"hr-2 bg-dark\" />\r\n<div class=\"product-type-holder half-width mx-auto d-flex justify-content-between\">\r\n    <h3 class=\"text-center\">Title: ");
            EndContext();
            BeginContext(356, 11, false);
#line 12 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                              Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(367, 137, true);
            WriteLiteral("</h3>\r\n</div>\r\n<hr class=\"hr-2 bg-dark\" />\r\n<div class=\"product-description-holder\">\r\n    <p class=\"text-center mt-4\">\r\n        Content: ");
            EndContext();
            BeginContext(505, 13, false);
#line 17 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
            Write(Model.Content);

#line default
#line hidden
            EndContext();
            BeginContext(518, 73, true);
            WriteLiteral("\r\n    </p>\r\n</div>\r\n<hr class=\"hr-2 bg-dark\" />\r\n<h3 class=\"text-center\">");
            EndContext();
            BeginContext(592, 16, false);
#line 21 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                   Write(Model.CreateDate);

#line default
#line hidden
            EndContext();
            BeginContext(608, 31, true);
            WriteLiteral("</h3>\r\n<h3 class=\"text-center\">");
            EndContext();
            BeginContext(640, 12, false);
#line 22 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                   Write(Model.Author);

#line default
#line hidden
            EndContext();
            BeginContext(652, 83, true);
            WriteLiteral("</h3>\r\n<div class=\"product-action-holder mt-4 d-flex justify-content-around\">\r\n    ");
            EndContext();
            BeginContext(735, 113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b4c46547f7e04d09de43717e098b224f9176e017215", async() => {
                BeginContext(840, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 24 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                                                         WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(848, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(854, 117, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8b4c46547f7e04d09de43717e098b224f9176e019873", async() => {
                BeginContext(961, 6, true);
                WriteLiteral("Delete");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 25 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
                                                           WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(971, 12, true);
            WriteLiteral("\r\n\r\n</div>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1001, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1008, 52, false);
#line 29 "C:\Users\john\Desktop\Share-Travel-System\src\ShareTravelSystem.Web\Views\Announcement\Details.cshtml"
Write(await Html.PartialAsync("_ValidationScriptsPartial"));

#line default
#line hidden
                EndContext();
                BeginContext(1060, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(1065, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShareTravelSystem.ViewModels.Announcement.DetailsAnnouncementViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
