using System.Threading.Tasks;
using UnityEngine.Animations;
using SPACS.SDK.CharacterController;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// This avatar controller attaches a <see cref="ParentConstraint"> to each node specified in the <see cref="AvatarControllerBase.CharacterReference", 
    /// and, for each <see cref="ParentConstraint">, sets the corresponding element in the <see cref="AvatarControllerBase.SourceCharacterController"> as constraint
    /// </summary>
    public class ParentConstraintAvatarController : AvatarControllerBase
    {
        public override async Task Setup(CharacterControllerBase sourceController)
        {
            await base.Setup(sourceController);

            if (characterReference.PivotReference && sourceController.PivotReference)
            {
                ParentConstraint pivotConstraint = characterReference.PivotReference.gameObject.AddComponent<ParentConstraint>();
                pivotConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.PivotReference, weight = 1 });
                pivotConstraint.constraintActive = true;
            }

            if (characterReference.HeadReference && sourceController.HeadReference)
            {
                ParentConstraint headParentConstraint = characterReference.HeadReference.gameObject.AddComponent<ParentConstraint>();
                headParentConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.HeadReference, weight = 1 });
                headParentConstraint.constraintActive = true;
            }

            if (characterReference.LeftInteractorReference && sourceController.LeftInteractorReference)
            {
                ParentConstraint leftHandConstraint = characterReference.LeftInteractorReference.gameObject.AddComponent<ParentConstraint>();
                leftHandConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.LeftInteractorReference, weight = 1 });
                leftHandConstraint.constraintActive = true;
            }

            if (characterReference.RightInteractorReference && sourceController.RightInteractorReference)
            {
                ParentConstraint rightHandConstraint = characterReference.RightInteractorReference.gameObject.AddComponent<ParentConstraint>();
                rightHandConstraint.AddSource(new ConstraintSource { sourceTransform = sourceController.RightInteractorReference, weight = 1 });
                rightHandConstraint.constraintActive = true;
            }
        }
    }
}
