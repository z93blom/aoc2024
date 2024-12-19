using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using AngleSharp.Common;

namespace AdventOfCode.Solutions.Y2024.Day17;

[RegisterKeyedTransient("2024-17")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 17;
    public string GetName() => "Chronospatial Computer";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var i = input.Integers().ToArray();

        var program = new Program()
        {
            A = i[0],
            B = i[1],
            C = i[2],
            Instructions = i.Skip(3).ToArray(),
        };

        program.Run();
        return string.Join(",", program.Output);
    }

    public class Program
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int[] Instructions { get; set; } = [];

        public List<int> Output { get; } = [];

        private int InstructionPointer { get; set; } = 0;

        private int GetComboOperand(Instruction instruction)
        {
            return instruction.Operand switch
            {
                0 => instruction.Operand,
                1 => instruction.Operand,
                2 => instruction.Operand,
                3 => instruction.Operand,
                4 => A,
                5 => B,
                6 => C,
                _ => throw new ArgumentOutOfRangeException()

            };
        }

        public void Run()
        {
            while (true)
            {
                if (InstructionPointer >= Instructions.Length)
                {
                    return;
                }

                var instruction = new Instruction((OpCode)Instructions[InstructionPointer],
                    Instructions[InstructionPointer + 1]);


                switch (instruction.OpCode)
                {
                    case OpCode.Adv:
                        Adv(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Bxl:
                        InstructionPointer += 2;
                        Bxl(instruction);
                        break;

                    case OpCode.Bst:
                        Bst(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Jnz:
                        Jnz(instruction);
                        break;

                    case OpCode.Bxc:
                        Bxc(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Out:
                        Out(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Bdv:
                        Bdv(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Cdv:
                        Cdv(instruction);
                        InstructionPointer += 2;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private bool OutputEqualsInstructions()
        {
            return Output.Count == Instructions.Length && 
                   Output.Index().All(t => t.Item == Instructions[t.Index]);
        }

        private bool OutputEqualsInstructionsSoFar()
        {
            return Output.Index().All(t => t.Item == Instructions[t.Index]);
        }

        public bool Run2()
        {
            while (true)
            {
                if (InstructionPointer >= Instructions.Length)
                {
                    return OutputEqualsInstructions();
                }

                if (Output.Count > Instructions.Length)
                {
                    return false;
                }

                if (!OutputEqualsInstructionsSoFar())
                {
                    return false;
                }

                var instruction = new Instruction((OpCode)Instructions[InstructionPointer],
                    Instructions[InstructionPointer + 1]);


                switch (instruction.OpCode)
                {
                    case OpCode.Adv:
                        Adv(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Bxl:
                        InstructionPointer += 2;
                        Bxl(instruction);
                        break;

                    case OpCode.Bst:
                        Bst(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Jnz:
                        Jnz(instruction);
                        break;

                    case OpCode.Bxc:
                        Bxc(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Out:
                        Out(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Bdv:
                        Bdv(instruction);
                        InstructionPointer += 2;
                        break;

                    case OpCode.Cdv:
                        Cdv(instruction);
                        InstructionPointer += 2;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Cdv(Instruction instruction)
        {
            var numerator = A;
            var denominator = (int)Math.Pow(2, GetComboOperand(instruction));
            C = numerator / denominator;
        }

        private void Bdv(Instruction instruction)
        {
            var numerator = A;
            var denominator = (int)Math.Pow(2, GetComboOperand(instruction));
            B = numerator / denominator;
        }

        private void Out(Instruction instruction)
        {
            Output.Add(GetComboOperand(instruction) % 8);
        }

        private void Bxc(Instruction instruction)
        {
            B ^= C;
        }

        private void Jnz(Instruction instruction)
        {
            if (A == 0)
            {
                InstructionPointer += 2;
                return;
            }

            InstructionPointer = instruction.Operand;
        }

        public void Adv(Instruction instruction)
        {
            var numerator = A;
            var denominator = (int)Math.Pow(2, GetComboOperand(instruction));
            A = numerator / denominator;
        }

        public void Bxl(Instruction instruction)
        {
            B ^= instruction.Operand;
        }

        public void Bst(Instruction instruction)
        {
            B = GetComboOperand(instruction) % 8;
        }

    }

    public enum OpCode
    {
        Adv = 0,
        Bxl = 1,
        Bst = 2,
        Jnz = 3,
        Bxc = 4,
        Out = 5,
        Bdv = 6,
        Cdv = 7,
    }

    public record struct Instruction(OpCode OpCode, int Operand);

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        return 0;
    }
}