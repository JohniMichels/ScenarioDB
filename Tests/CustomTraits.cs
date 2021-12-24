using System.Diagnostics;
using Xunit;
using Xunit.Categories;

namespace TestScenarioDB;
public class ThirdPartyTestAttribute : CategoryAttribute
{
    public ThirdPartyTestAttribute() : base("Third Party")
    { }
}
