// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\General\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace JIT.HardwareIntrinsics.General
{
    public static partial class Program
    {
        private static void GetAndWithLowerAndUpperUInt32()
        {
            var test = new VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperUInt32();

            // Validates basic functionality works
            test.RunBasicScenario();

            // Validates calling via reflection works
            test.RunReflectionScenario();

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperUInt32
    {
        private static readonly int LargestVectorSize = 32;

        private static readonly int ElementCount = Unsafe.SizeOf<Vector256<UInt32>>() / sizeof(UInt32);

        public bool Succeeded { get; set; } = true;

        public void RunBasicScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario));

            UInt32[] values = new UInt32[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetUInt32();
            }

            Vector256<UInt32> value = Vector256.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            Vector128<UInt32> lowerResult = value.GetLower();
            Vector128<UInt32> upperResult = value.GetUpper();
            ValidateGetResult(lowerResult, upperResult, values);

            Vector256<UInt32> result = value.WithLower(upperResult);
            result = result.WithUpper(lowerResult);
            ValidateWithResult(result, values);
        }

        public void RunReflectionScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario));

            UInt32[] values = new UInt32[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetUInt32();
            }

            Vector256<UInt32> value = Vector256.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            object lowerResult = typeof(Vector256<UInt32>)
                                    .GetMethod(nameof(Vector256<UInt32>.GetLower), new Type[] { })
                                    .Invoke(value, new object[] { });
            object upperResult = typeof(Vector256<UInt32>)
                                    .GetMethod(nameof(Vector256<UInt32>.GetUpper), new Type[] { })
                                    .Invoke(value, new object[] { });
            ValidateGetResult((Vector128<UInt32>)(lowerResult), (Vector128<UInt32>)(upperResult), values);

            object result = typeof(Vector256<UInt32>)
                                .GetMethod(nameof(Vector256<UInt32>.WithLower), new Type[] { typeof(Vector128<UInt32>) })
                                .Invoke(value, new object[] { upperResult });
            result = typeof(Vector256<UInt32>)
                        .GetMethod(nameof(Vector256<UInt32>.WithUpper), new Type[] { typeof(Vector128<UInt32>) })
                        .Invoke(result, new object[] { lowerResult });
            ValidateWithResult((Vector256<UInt32>)(result), values);
        }

        private void ValidateGetResult(Vector128<UInt32> lowerResult, Vector128<UInt32> upperResult, UInt32[] values, [CallerMemberName] string method = "")
        {
            UInt32[] lowerElements = new UInt32[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<UInt32, byte>(ref lowerElements[0]), lowerResult);

            UInt32[] upperElements = new UInt32[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<UInt32, byte>(ref upperElements[0]), upperResult);

            ValidateGetResult(lowerElements, upperElements, values, method);
        }

        private void ValidateGetResult(UInt32[] lowerResult, UInt32[] upperResult, UInt32[] values, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            for (int i = 0; i < ElementCount / 2; i++)
            {
                if (lowerResult[i] != values[i])
                {
                    succeeded = false;
                    break;
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<UInt32>.GetLower(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", lowerResult)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }

            succeeded = true;

            for (int i = ElementCount / 2; i < ElementCount; i++)
            {
                if (upperResult[i - (ElementCount / 2)] != values[i])
                {
                    succeeded = false;
                    break;
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<UInt32>.GetUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", upperResult)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }

        private void ValidateWithResult(Vector256<UInt32> result, UInt32[] values, [CallerMemberName] string method = "")
        {
            UInt32[] resultElements = new UInt32[ElementCount];
            Unsafe.WriteUnaligned(ref Unsafe.As<UInt32, byte>(ref resultElements[0]), result);
            ValidateWithResult(resultElements, values, method);
        }

        private void ValidateWithResult(UInt32[] result, UInt32[] values, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            for (int i = 0; i < ElementCount / 2; i++)
            {
                if (result[i] != values[i + (ElementCount / 2)])
                {
                    succeeded = false;
                    break;
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<UInt32.WithLower(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }

            succeeded = true;

            for (int i = ElementCount / 2; i < ElementCount; i++)
            {
                if (result[i] != values[i - (ElementCount / 2)])
                {
                    succeeded = false;
                    break;
                }
            }

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<UInt32.WithUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
