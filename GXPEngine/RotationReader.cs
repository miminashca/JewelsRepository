namespace GXPEngine
{
    public class RotationReader : GameObject
    {
        private Controller controller;
        public float controllerRotation;
        public RotationReader(Controller pController)
        {
            controller = pController;
            controllerRotation = controller.GetRotation();
        }

        void Update()
        {
            controllerRotation = controller.GetRotation();
        }
    }
}