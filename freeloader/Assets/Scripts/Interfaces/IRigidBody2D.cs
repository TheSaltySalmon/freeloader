using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace FreeLoader.Interfaces
{
    public interface IRigidbody2D
    {
        #region Properties
        float angularDrag { get; set; }
        float angularVelocity { get; set; }
        int attachedColliderCount { get; }
        RigidbodyType2D bodyType { get; set; }
        Vector2 centerOfMass { get; set; }
        CollisionDetectionMode2D collisionDetectionMode { get; set; }
        RigidbodyConstraints2D constraints { get; set; }
        float drag { get; set; }
        bool fixedAngle { get; set; }
        bool freezeRotation { get; set; }
        float gravityScale { get; set; }
        float inertia { get; set; }
        RigidbodyInterpolation2D interpolation { get; set; }
        bool isKinematic { get; set; }
        float mass { get; set; }
        Vector2 position { get; set; }
        float rotation { get; set; }
        PhysicsMaterial2D sharedMaterial { get; set; }
        bool simulated { get; set; }
        RigidbodySleepMode2D sleepMode { get; set; }
        bool useAutoMass { get; set; }
        bool useFullKinematicContacts { get; set; }
        Vector2 velocity { get; set; }
        Vector2 worldCenterOfMass { get; }
        #endregion
        #region Methods
        void AddForce(Vector2 force);
        void AddForce(Vector2 force, ForceMode2D mode);
        void AddForceAtPosition(Vector2 force, Vector2 position);
        void AddForceAtPosition(Vector2 force, Vector2 position, ForceMode2D mode);
        void AddRelativeForce(Vector2 relativeForce);
        void AddRelativeForce(Vector2 relativeForce, ForceMode2D mode);
        void AddTorque(float torque);
        void AddTorque(float torque, ForceMode2D mode);
        int Cast(Vector2 direction, RaycastHit2D[] results);
        int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results);
        int Cast(Vector2 direction, RaycastHit2D[] results, float distance);
        int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance);
        ColliderDistance2D Distance(Collider2D collider);
        int GetAttachedColliders(Collider2D[] results);
        int GetContacts(Collider2D[] colliders);
        int GetContacts(ContactPoint2D[] contacts);
        int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders);
        int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts);
        Vector2 GetPoint(Vector2 point);
        Vector2 GetPointVelocity(Vector2 point);
        Vector2 GetRelativePoint(Vector2 relativePoint);
        Vector2 GetRelativePointVelocity(Vector2 relativePoint);
        Vector2 GetRelativeVector(Vector2 relativeVector);
        Vector2 GetVector(Vector2 vector);
        bool IsAwake();
        bool IsSleeping();
        bool IsTouching(Collider2D collider);
        bool IsTouching(ContactFilter2D contactFilter);
        bool IsTouching(Collider2D collider, ContactFilter2D contactFilter);
        bool IsTouchingLayers();
        bool IsTouchingLayers(int layerMask);
        void MovePosition(Vector2 position);
        void MoveRotation(float angle);
        int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results);
        bool OverlapPoint(Vector2 point);
        void Sleep();
        void WakeUp();
        #endregion
    }
}
