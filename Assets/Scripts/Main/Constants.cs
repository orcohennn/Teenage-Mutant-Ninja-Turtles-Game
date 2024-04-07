using System;

namespace Main
{
    public static class Constants
    {
        // UI Constants
        public const int NumOfUiZeros = 7;

        // Player Constants
        public const int MaxPlayerLife = 80;
        public const float PlayerSpeed = 3f;
        public const float DieDelayTime = 3f;
        public const float PlayerJumpPower = 11f;
        public const float HighJumpAnimThreshold = 4.8f;
        public const String PlayerTag = "NinjaTurtle";
        public const String PlayerSwordTag = "Sword";
        public const String GroundedAnimBool = "IsGrounded";
        public const String HorizontalAnimBool = "MoveHorizontal";
        public const String JumpingAnimBool = "IsJumping";
        public const String AttackingAnimBool = "IsAttack";

        // Little Monster Constants
        public const int LittleMonsterPoints = 50;
        public const float LittleMonsterMovementSpeed = 2f;
        public const String MonsterTag = "Monster";
        public const String MonsterDieBoolAnimation = "IsDie";
        
        // Enemy Monster Constants
        public const String ThrowBoolAnimation = "IsThrow";
        public const String MovingBoolAnimation = "IsMoving";
        
        
        // Camera Constants
        public const float CameraMinX = -0.13f;
        public const float CameraMaxX = 4.49f;
        public const float CameraMinY = -2.2f;
        public const float CameraMaxY = 0.08f;
        public const float UnderCameraMinX = -0.36f;
        public const float UnderCameraMaxX = 28.4f;
        public const float UnderCameraMinY = 0.03f;
        public const float UnderCameraMaxY = 0.35f;
        public const float CameraSmooth = 0.125f;
        public const float CameraZOffset = -10f;

        // Movement Constants
        public const String HorizontalDirection = "Horizontal";
        public const String VerticalDirection = "Vertical";
        public const String MoveDownAnimationBool = "MoveDown";
        public const String MoveUpAnimationBool = "MoveUp";
        public const String MoveHorizontalAnimationBool = "MoveHorizontal";
        public const String IdleBlendTree = "IdleBlend";

        // Scene Constants
        public const String OverWorldScene = "OverWorldScene";
        public const String GameOverScene = "GameOverScreen";
    
        // Gameplay Constants
        public const float BlinkTime = 0.1f;
        public const float ChangeSceneTime = 2f;
        public const float StartGamePosX = -1.42f;
        public const float StartGamePosY = 1.9f;
        public const String LadderTag = "Ladder";
        public const String WallTag = "Wall";
        public const String LadderIdleBoolAnimation = "LadderIdle";
        public const String LadderClimbBoolAnimation = "IsClimb";
        public const String PointsTextPrefix = "PTS. ";
        public const String JumpButton = "Jump";

        // Enemy Constants
        public const int HumanEnemyPoints = 150;
        
        // Restart Screen
        public const String Continue = "Continue";
        public const String End = "End";
    }
}
