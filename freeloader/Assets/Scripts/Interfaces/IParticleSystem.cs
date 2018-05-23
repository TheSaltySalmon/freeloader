using System;
using UnityEngine;

namespace FreeLoader.Interfaces
{
    public interface IParticleSystem
    {
        void Clear();
        void Clear(bool withChildren);
        ParticleSystem.CollisionModule collision { get; }
        ParticleSystem.ColorBySpeedModule colorBySpeed { get; }
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime { get; }
        ParticleSystem.CustomDataModule customData { get; }
        float duration { get; }
        ParticleSystem.EmissionModule emission { get; }
        float emissionRate { get; set; }
        void Emit(int count);
        void Emit(ParticleSystem.EmitParams emitParams, int count);
        void Emit(ParticleSystem.Particle particle);
        void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color);
        bool enableEmission { get; set; }
        ParticleSystem.ExternalForcesModule externalForces { get; }
        ParticleSystem.ForceOverLifetimeModule forceOverLifetime { get; }
        int GetCustomParticleData(System.Collections.Generic.List<Vector4> customData, ParticleSystemCustomData streamIndex);
        int GetParticles(ParticleSystem.Particle[] particles);
        float gravityModifier { get; set; }
        ParticleSystem.InheritVelocityModule inheritVelocity { get; }
        bool IsAlive();
        bool IsAlive(bool withChildren);
        bool isEmitting { get; }
        bool isPaused { get; }
        bool isPlaying { get; }
        bool isStopped { get; }
        ParticleSystem.LightsModule lights { get; }
        ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime { get; }
        bool loop { get; set; }
        ParticleSystem.MainModule main { get; }
        int maxParticles { get; set; }
        ParticleSystem.NoiseModule noise { get; }
        int particleCount { get; }
        void Pause();
        void Pause(bool withChildren);
        void Play();
        void Play(bool withChildren);
        float playbackSpeed { get; set; }
        bool playOnAwake { get; set; }
        uint randomSeed { get; set; }
        ParticleSystem.RotationBySpeedModule rotationBySpeed { get; }
        ParticleSystem.RotationOverLifetimeModule rotationOverLifetime { get; }
        int safeCollisionEventSize { get; }
        ParticleSystemScalingMode scalingMode { get; set; }
        void SetCustomParticleData(System.Collections.Generic.List<Vector4> customData, ParticleSystemCustomData streamIndex);
        void SetParticles(ParticleSystem.Particle[] particles, int size);
        ParticleSystem.ShapeModule shape { get; }
        void Simulate(float t);
        void Simulate(float t, bool withChildren);
        void Simulate(float t, bool withChildren, bool restart);
        void Simulate(float t, bool withChildren, bool restart, bool fixedTimeStep);
        ParticleSystemSimulationSpace simulationSpace { get; set; }
        ParticleSystem.SizeBySpeedModule sizeBySpeed { get; }
        ParticleSystem.SizeOverLifetimeModule sizeOverLifetime { get; }
        Color startColor { get; set; }
        float startDelay { get; set; }
        float startLifetime { get; set; }
        float startRotation { get; set; }
        Vector3 startRotation3D { get; set; }
        float startSize { get; set; }
        float startSpeed { get; set; }
        void Stop();
        void Stop(bool withChildren);
        void Stop(bool withChildren, ParticleSystemStopBehavior stopBehavior);
        ParticleSystem.SubEmittersModule subEmitters { get; }
        ParticleSystem.TextureSheetAnimationModule textureSheetAnimation { get; }
        float time { get; set; }
        ParticleSystem.TrailModule trails { get; }
        ParticleSystem.TriggerModule trigger { get; }
        bool useAutoRandomSeed { get; set; }
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime { get; }
    }
}
