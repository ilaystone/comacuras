#pragma checksum "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "962eedbb8c7e56de95652de5f53c716fa4288012"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ComaCuras.web.Pages.AjaxCall.Pages_AjaxCall_GetAgents), @"mvc.1.0.razor-page", @"/Pages/AjaxCall/GetAgents.cshtml")]
namespace ComaCuras.web.Pages.AjaxCall
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
#line 1 "C:\ilay\comacuras.web\Pages\_ViewImports.cshtml"
using ComaCuras.web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"962eedbb8c7e56de95652de5f53c716fa4288012", @"/Pages/AjaxCall/GetAgents.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"970cabe04e8a380d7917197d820388d6a7e634c7", @"/Pages/_ViewImports.cshtml")]
    public class Pages_AjaxCall_GetAgents : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml"
  
    Layout = "";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml"
 foreach (var item in Model.Agents)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <input type=\"radio\" id=\"agent-picker\"");
            BeginWriteAttribute("value", " value=\"", 169, "\"", 185, 1);
#nullable restore
#line 10 "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml"
WriteAttributeValue("", 177, item.Id, 177, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"agent-picker\"/>\r\n    <label>");
#nullable restore
#line 11 "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml"
      Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n    <br />\r\n");
#nullable restore
#line 13 "C:\ilay\comacuras.web\Pages\AjaxCall\GetAgents.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ComaCuras.web.Pages.AjaxCall.GetAgentsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ComaCuras.web.Pages.AjaxCall.GetAgentsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ComaCuras.web.Pages.AjaxCall.GetAgentsModel>)PageContext?.ViewData;
        public ComaCuras.web.Pages.AjaxCall.GetAgentsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
