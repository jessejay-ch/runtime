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
        private static void GetAndWithLowerAndUpperInt16()
        {
            var test = new VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperInt16();

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

    public sealed unsafe class VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperInt16
    {
        private static readonly int LargestVectorSize = 16;

        private static readonly int ElementCount = Unsafe.SizeOf<Vector128<Int16>>() / sizeof(Int16);

        public bool Succeeded { get; set; } = true;

        public void RunBasicScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario));

            Int16[] values = new Int16[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetInt16();
            }

            Vector128<Int16> value = Vector128.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            Vector64<Int16> lowerResult = value.GetLower();
            Vector64<Int16> upperResult = value.GetUpper();
            ValidateGetResult(lowerResult, upperResult, values);

            Vector128<Int16> result = value.WithLower(upperResult);
            result = result.WithUpper(lowerResult);
            ValidateWithResult(result, values);
        }

        public void RunReflectionScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario));

            Int16[] values = new Int16[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetInt16();
            }

            Vector128<Int16> value = Vector128.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            object lowerResult = typeof(Vector128<Int16>)
                                    .GetMethod(nameof(Vector128<Int16>.GetLower), new Type[] { })
                                    .Invoke(value, new object[] { });
            object upperResult = typeof(Vector128<Int16>)
                                    .GetMethod(nameof(Vector128<Int16>.GetUpper), new Type[] { })
                                    .Invoke(value, new object[] { });
            ValidateGetResult((Vector64<Int16>)(lowerResult), (Vector64<Int16>)(upperResult), values);

            object result = typeof(Vector128<Int16>)
                                .GetMethod(nameof(Vector128<Int16>.WithLower), new Type[] { typeof(Vector64<Int16>) })
                                .Invoke(value, new object[] { upperResult });
            result = typeof(Vector128<Int16>)
                        .GetMethod(nameof(Vector128<Int16>.WithUpper), new Type[] { typeof(Vector64<Int16>) })
                        .Invoke(result, new object[] { lowerResult });
            ValidateWithResult((Vector128<Int16>)(result), values);
        }

        private void ValidateGetResult(Vector64<Int16> lowerResult, Vector64<Int16> upperResult, Int16[] values, [CallerMemberName] string method = "")
        {
            Int16[] lowerElements = new Int16[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<Int16, byte>(ref lowerElements[0]), lowerResult);

            Int16[] upperElements = new Int16[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<Int16, byte>(ref upperElements[0]), upperResult);

            ValidateGetResult(lowerElements, upperElements, values, method);
        }

        private void ValidateGetResult(Int16[] lowerResult, Int16[] upperResult, Int16[] values, [CallerMemberName] string method = "")
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
                TestLibrary.TestFramework.LogInformation($"Vector128<Int16>.GetLower(): {method} failed:");
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
                TestLibrary.TestFramework.LogInformation($"Vector128<Int16>.GetUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", upperResult)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }

        private void ValidateWithResult(Vector128<Int16> result, Int16[] values, [CallerMemberName] string method = "")
        {
            Int16[] resultElements = new Int16[ElementCount];
            Unsafe.WriteUnaligned(ref Unsafe.As<Int16, byte>(ref resultElements[0]), result);
            ValidateWithResult(resultElements, values, method);
        }

        private void ValidateWithResult(Int16[] result, Int16[] values, [CallerMemberName] string method = "")
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
                TestLibrary.TestFramework.LogInformation($"Vector128<Int16.WithLower(): {method} failed:");
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
                TestLibrary.TestFramework.LogInformation($"Vector128<Int16.WithUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
