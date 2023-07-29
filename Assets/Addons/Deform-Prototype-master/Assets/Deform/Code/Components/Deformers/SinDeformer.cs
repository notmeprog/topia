﻿using System.Runtime.CompilerServices;
using UnityEngine;
using Deform.Math.Trig;

namespace Deform.Deformers
{
	public class SinDeformer : DeformerComponent
	{
		public float offset;
		public float speed;
		public bool usePosition;
		public bool useRotation;
		public bool useScale;

		public Axis along = Axis.X;
		public Axis by = Axis.Y;

		public Sin sin = new Sin () { amplitude = 0.25f, frequency = 5f };

		private Vector3 axisOffset;
		private float speedOffset;

		public override void PreModify ()
		{
			speedOffset += Manager.SyncedDeltaTime * speed;

			if (sin.frequency == 0f)
				sin.frequency = 0.0001f;

			switch (along)
			{
				case Axis.X:
					axisOffset = Vector3.right * offset / sin.frequency;
					break;
				case Axis.Y:
					axisOffset = Vector3.up * offset / sin.frequency;
					break;
				case Axis.Z:
					axisOffset = Vector3.forward * offset / sin.frequency;
					break;
			}
		}

		public override MeshData Modify (MeshData meshData, TransformData transformData, Bounds meshBounds)
		{
			for (int i = 0; i < meshData.Size; i++)
			{
				var samplePosition = meshData.vertices[i] + axisOffset;
				if (usePosition)
					samplePosition += transformData.position;
				if (useRotation)
					samplePosition = transformData.rotation * samplePosition;
				if (useScale)
					samplePosition.Scale (transformData.localScale);
				meshData.vertices[i] += Sin3D (samplePosition);
			}

			return meshData;
		}

		[MethodImplAttribute (MethodImplOptions.AggressiveInlining)]
		private Vector3 Sin3D (Vector3 sample)
		{
			var animatedOffset = offset + speedOffset;
			var byValue = 0f;
			switch (by)
			{
				case Axis.X:
					byValue = sample.x;
					break;
				case Axis.Y:
					byValue = sample.y;
					break;
				case Axis.Z:
					byValue = sample.z;
					break;
			}
			var value = sin.Solve (byValue, animatedOffset);
			switch (along)
			{
				case Axis.X:
					return new Vector3 (value, 0f, 0f);
				case Axis.Y:
					return new Vector3 (0f, value, 0f);
				default:
					return new Vector3 (0f, 0f, value);
			}
		}
	}
}