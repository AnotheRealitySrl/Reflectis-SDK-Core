using System.Threading.Tasks;
using UnityEngine.Animations;
using SPACS.SDK.CharacterController;

namespace SPACS.SDK.Avatars
{
    public class ParentConstraintAvatarController : AvatarControllerBase
    {
        public override async Task Setup(CharacterControllerBase sourceController)
        {
            await base.Setup(sourceController);

            if (sourceCharacter.PivotReference && sourceController.PivotReference)
            {
                ParentConstraint pivotConstraint = sourceCharacter.PivotReference.gameObject.AddComponent<ParentConstraint>();
                pivotConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.PivotReference, weight = 1 });
                pivotConstraint.constraintActive = true;
            }

            if (sourceCharacter.HeadReference && sourceController.HeadReference)
            {
                ParentConstraint headParentConstraint = sourceCharacter.HeadReference.gameObject.AddComponent<ParentConstraint>();
                headParentConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.HeadReference, weight = 1 });
                headParentConstraint.constraintActive = true;
            }

            if (sourceCharacter.LeftInteractorReference && sourceController.LeftInteractorReference)
            {
                ParentConstraint leftHandConstraint = sourceCharacter.LeftInteractorReference.gameObject.AddComponent<ParentConstraint>();
                leftHandConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.LeftInteractorReference, weight = 1 });
                leftHandConstraint.constraintActive = true;
            }

            if (sourceCharacter.RightInteractorReference && sourceController.RightInteractorReference)
            {
                ParentConstraint rightHandConstraint = sourceCharacter.RightInteractorReference.gameObject.AddComponent<ParentConstraint>();
                rightHandConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.RightInteractorReference, weight = 1 });
                rightHandConstraint.constraintActive = true;
            }
        }
    }
}
