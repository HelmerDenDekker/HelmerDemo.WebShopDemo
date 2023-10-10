using System.Collections;

namespace WSD.Catalog.Domain.UnitTests.nUnit;

public static class CatalogItemTestData
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData(200, 7);
            yield return new TestCaseData(100, 1);
            yield return new TestCaseData(2, 1);
            yield return new TestCaseData(1, 1
                );
        }
    }
}