using NUnit.Framework.Constraints;
using WSD.Common;

namespace WSD.Catalog.Domain.UnitTests.nUnit;

public class ResultConstraint : Constraint
{
    private readonly Result _expectedResult;

    public ResultConstraint(Result expectedResult)
    {
        _expectedResult = expectedResult;
    }

    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        if (actual is not Result actualResult)
        {
            return new ConstraintResult(this, actual, ConstraintStatus.Error);
        }

        if (_expectedResult.IsSuccess != actualResult.IsSuccess)
        {
            return new ConstraintResult(this, actualResult.IsSuccess, ConstraintStatus.Failure);
        }
        
        if (_expectedResult.StatusCode != actualResult.StatusCode)
        {
            return new ConstraintResult(this, actualResult.StatusCode, ConstraintStatus.Failure);
        }
        
        if (_expectedResult.Messages.Count != actualResult.Messages.Count)
        {
            return new ConstraintResult(this, actualResult.StatusCode, ConstraintStatus.Failure);
        }

        return new ConstraintResult(this, actualResult, ConstraintStatus.Success);
    }
}