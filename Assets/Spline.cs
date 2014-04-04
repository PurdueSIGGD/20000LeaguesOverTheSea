//Author: Fernando Zapata (fernando@cpudreams.com)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
     * Returns sequence generator from the first node, through each control point,
     * and to the last node. N points are generated between each node (slices)
     * using Catmull-Rom.
     */
public class Spline {
	
	public delegate Vector3 ToVector3<T>(T v);
	static Vector3 Identity(Vector3 v) {
		return v;
	}
	/**
	     * A Vector3[] variation of the Transform[] NewCatmullRom() function.
	     * Same functionality but using Vector3s to define curve.
	     */
	public static IEnumerable<Vector3> NewCatmullRom(Vector3[] points, int slices, bool loop) {
		return NewCatmullRom<Vector3>(points, Identity, slices, loop);
	}

	/**
	     * Generic catmull-rom spline sequence generator used to implement the
	     * Vector3[] and Transform[] variants. Normally you would not use this
	     * function directly.
	     */
	static IEnumerable<Vector3> NewCatmullRom<T>(IList nodes, ToVector3<T> toVector3, int slices, bool loop) {
		// need at least two nodes to spline between
		if (nodes.Count >= 2) {
			
			// yield the first point explicitly, if looping the first point
			// will be generated again in the step for loop when interpolating
			// from last point back to the first point
			yield return toVector3((T)nodes[0]);
			
			int last = nodes.Count - 1;
			for (int current = 0; loop || current < last; current++) {
				// wrap around when looping
				if (loop && current > last) {
					current = 0;
				}
				// handle edge cases for looping and non-looping scenarios
				// when looping we wrap around, when not looping use start for previous
				// and end for next when you at the ends of the nodes array
				int previous = (current == 0) ? ((loop) ? last : current) : current - 1;
				int start = current;
				int end = (current == last) ? ((loop) ? 0 : current) : current + 1;
				int next = (end == last) ? ((loop) ? 0 : end) : end + 1;
				
				// adding one guarantees yielding at least the end point
				int stepCount = slices + 1;
				for (int step = 1; step <= stepCount; step++) {
					yield return CatmullRom(toVector3((T)nodes[previous]),
					                        toVector3((T)nodes[start]),
					                        toVector3((T)nodes[end]),
					                        toVector3((T)nodes[next]),
					                        step, stepCount);
				}
			}
		}
	}

	/**
	     * A Vector3 Catmull-Rom spline. Catmull-Rom splines are similar to bezier
	     * splines but have the useful property that the generated curve will go
	     * through each of the control points.
	     *
	     * NOTE: The NewCatmullRom() functions are an easier to use alternative to this
	     * raw Catmull-Rom implementation.
	     *
	     * @param previous the point just before the start point or the start point
	     *                 itself if no previous point is available
	     * @param start generated when elapsedTime == 0
	     * @param end generated when elapsedTime >= duration
	     * @param next the point just after the end point or the end point itself if no
	     *             next point is available
	     */
	static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, 
	                          float elapsedTime, float duration) {
		// References used:
		// p.266 GemsV1
		//
		// tension is often set to 0.5 but you can use any reasonable value:
		// http://www.cs.cmu.edu/~462/projects/assn2/assn2/catmullRom.pdf
		//
		// bias and tension controls:
		// http://local.wasp.uwa.edu.au/~pbourke/miscellaneous/interpolation/
		
		float percentComplete = elapsedTime / duration;
		float percentCompleteSquared = percentComplete * percentComplete;
		float percentCompleteCubed = percentCompleteSquared * percentComplete;
		
		return previous * (-0.5f * percentCompleteCubed +
		                   percentCompleteSquared -
		                   0.5f * percentComplete) +
				start   * ( 1.5f * percentCompleteCubed +
			           -2.5f * percentCompleteSquared + 1.0f) +
				end     * (-1.5f * percentCompleteCubed +
				           2.0f * percentCompleteSquared +
				           0.5f * percentComplete) +
				next    * ( 0.5f * percentCompleteCubed -
				           0.5f * percentCompleteSquared);
	}

	static public Vector3[] somethingsomethingLine(IEnumerable<Vector3> list, float dist) {
		List<Vector3> newList = new List<Vector3>();

		Vector3 last = Vector3.zero;
		foreach(Vector3 v in list) {
			if (last == Vector3.zero)
				last = v;

			newList.Add(v);
			dist -= Vector3.Distance(last, v);
			if (dist < 0) {
				break;
			}
			last = v;
		}
		return newList.ToArray();
	}
}