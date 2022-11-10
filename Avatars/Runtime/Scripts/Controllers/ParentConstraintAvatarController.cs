using SPACS.SDK.CharacterController;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Animations;

namespace SPACS.SDK.Avatars
{
    public class ParentConstraintAvatarController : AvatarControllerBase
    {
        public override async Task Setup(CharacterControllerBase source)
        {
            await base.Setup(source);

            if (PivotReference && source.PivotReference)
            {
                ParentConstraint pivotConstraint = PivotReference.gameObject.AddComponent<ParentConstraint>();
                pivotConstraint.AddSource(new ConstraintSource { sourceTransform = source.PivotReference, weight = 1 });
                pivotConstraint.constraintActive = true;
            }

            if (HeadReference && source.HeadReference)
            {
                ParentConstraint headParentConstraint = HeadReference.gameObject.AddComponent<ParentConstraint>();
                headParentConstraint.AddSource(new ConstraintSource { sourceTransform = source.HeadReference, weight = 1 });
                headParentConstraint.constraintActive = true;
            }

            ParentConstraint leftHandConstraint = LeftInteractorReference.gameObject.AddComponent<ParentConstraint>();
            leftHandConstraint.AddSource(new ConstraintSource { sourceTransform = source.LeftInteractorReference, weight = 1 });
            leftHandConstraint.constraintActive = true;

            ParentConstraint rightHandConstraint = RightInteractorReference.gameObject.AddComponent<ParentConstraint>();
            rightHandConstraint.AddSource(new ConstraintSource { sourceTransform = source.RightInteractorReference, weight = 1 });
            rightHandConstraint.constraintActive = true;
        }
    }
}
