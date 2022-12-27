namespace Dustyroom {
public class CustomUserCharacterController : UserCharacterController {
    public bool autoMove = true;
    public bool autoJump = true;

    private void LateUpdate() {
        if (autoMove) {
            input.x = 1;
        }

        if (autoJump) {
            input.y = 1;
        }
    }
}
}