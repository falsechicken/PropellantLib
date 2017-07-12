// /*****
// * Math.cs
// *
// * Author:
// *     False_Chicken <jmdevsupport@gmail.com>
// *
// * Copyright (c) 2017 False_Chicken
// *
// * This program is free software; you can redistribute it and/or
// * modify it under the terms of the GNU General Public License
// * as published by the Free Software Foundation; either version 2
// * of the License, or (at your option) any later version.
// *
// * This program is distributed in the hope that it will be useful,
// * but WITHOUT ANY WARRANTY; without even the implied warranty of
// * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// * GNU General Public License for more details.
// * 
// * You should have received a copy of the GNU General Public License
// * along with this program; if not, Get it here: https://www.gnu.org/licenses/gpl-2.0.html
// *****/
//
using System;
using UnityEngine;

using Rocket.Core.Logging;

namespace FC.PropellantLib
{
	public static class Math
	{
		/**
		 * Compare two Vector3s. Due to floating points not being precise an allowed differential
		 * must be provided.
		 */
		public static bool CompareVector3(Vector3 _v1, Vector3 _v2, float _allowedVariation) {
			var dx = _v1.x - _v2.x;
			if (Mathf.Abs(dx) > _allowedVariation)
				return false;

			var dy = _v2.y - _v2.y;
			if (Mathf.Abs(dy) > _allowedVariation)
				return false;

			var dz = _v1.z - _v2.z;
			if (Mathf.Abs (dz) > _allowedVariation)
				return false;

			return true;
		}
	}
}

