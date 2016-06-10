using System;
using System.Reflection;

namespace WebApi_SAP_RFC_rfc_read_table.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}