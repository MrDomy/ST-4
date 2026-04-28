using System;
using Stateless;

namespace BugPro
{
    public class Bug
    {
        public enum State
        {
            New,
            Analysis,
            Fixing,
            Verification,
            Closed,
            Return,
            Reopened,
            NeedInfo,
            OtherProduct,
            NeedDecisionLater,
            NotABug,
            WontFix,
            Duplicate,
            NotReproducible,
            NoTime,
            SeparateSolution,
            MoreInfo
        }

        public enum Trigger
        {
            StartAnalysis,
            StartFixing,
            StartFix,
            MarkFixed,
            Approve,
            Reject,
            Return,
            MarkNotABug,
            MarkWontFix,
            MarkDuplicate,
            MarkNotReproducible,
            ProblemSolved,
            ProblemNotSolved,
            ReturnToAnalysis,
            Reopen,
            NeedInfo,
            OtherProduct,
            NeedDecisionLater,
            NoTimeNow,
            NeedSeparateSolution,
            ProblemOtherProduct,
            NeedMoreInfo,
            ConfirmOK,
            ConfirmNotOK
        }

        private readonly StateMachine<State, Trigger> _machine;

        public State CurrentState => _machine.State;

        public Bug()
        {
            _machine = new StateMachine<State, Trigger>(State.New);

            _machine.Configure(State.New)
                .Permit(Trigger.StartAnalysis, State.Analysis);

            _machine.Configure(State.Analysis)
                .Permit(Trigger.StartFixing, State.Fixing)
                .Permit(Trigger.StartFix, State.Fixing)
                .Permit(Trigger.MarkNotABug, State.NotABug)
                .Permit(Trigger.MarkWontFix, State.WontFix)
                .Permit(Trigger.MarkDuplicate, State.Duplicate)
                .Permit(Trigger.MarkNotReproducible, State.NotReproducible)
                .Permit(Trigger.NeedInfo, State.NeedInfo)
                .Permit(Trigger.OtherProduct, State.OtherProduct)
                .Permit(Trigger.NeedDecisionLater, State.NeedDecisionLater);

            _machine.Configure(State.Fixing)
                .Permit(Trigger.MarkFixed, State.Verification)
                .Permit(Trigger.ProblemSolved, State.Closed)
                .Permit(Trigger.ProblemNotSolved, State.Return)
                .Permit(Trigger.MarkNotReproducible, State.NotReproducible)
                .Permit(Trigger.NoTimeNow, State.NoTime)
                .Permit(Trigger.NeedSeparateSolution, State.SeparateSolution)
                .Permit(Trigger.ProblemOtherProduct, State.OtherProduct)
                .Permit(Trigger.NeedMoreInfo, State.MoreInfo);

            _machine.Configure(State.NotReproducible)
                .Permit(Trigger.ConfirmOK, State.Closed)
                .Permit(Trigger.ConfirmNotOK, State.Return);

            _machine.Configure(State.NeedInfo)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis)
                .Permit(Trigger.Return, State.Return);

            _machine.Configure(State.OtherProduct)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis)
                .Permit(Trigger.Return, State.Return);

            _machine.Configure(State.NeedDecisionLater)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis)
                .Permit(Trigger.Return, State.Return);

            _machine.Configure(State.NotABug)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.WontFix)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.Duplicate)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.Return)
                .Permit(Trigger.StartAnalysis, State.Analysis);

            _machine.Configure(State.NoTime)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.SeparateSolution)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.OtherProduct)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.MoreInfo)
                .Permit(Trigger.ReturnToAnalysis, State.Analysis);

            _machine.Configure(State.Closed)
                .Permit(Trigger.Reopen, State.Reopened);

            _machine.Configure(State.Reopened)
                .Permit(Trigger.StartAnalysis, State.Analysis);
        }

        public void StartAnalysis()
        {
            _machine.Fire(Trigger.StartAnalysis);
        }

        public void StartTriage()
        {
            StartAnalysis();
        }

        public void StartFixing()
        {
            _machine.Fire(Trigger.StartFixing);
        }

        public void StartFix()
        {
            _machine.Fire(Trigger.StartFix);
        }

        public void MarkNotABug()
        {
            _machine.Fire(Trigger.MarkNotABug);
        }

        public void MarkNotBug()
        {
            MarkNotABug();
        }

        public void MarkWontFix()
        {
            _machine.Fire(Trigger.MarkWontFix);
        }

        public void MarkDuplicate()
        {
            _machine.Fire(Trigger.MarkDuplicate);
        }

        public void MarkNotReproducible()
        {
            _machine.Fire(Trigger.MarkNotReproducible);
        }

        public void MarkNotRepro()
        {
            MarkNotReproducible();
        }

        public void MarkFixed()
        {
            _machine.Fire(Trigger.MarkFixed);
        }

        public void Approve()
        {
            _machine.Fire(Trigger.Approve);
        }

        public void Reject()
        {
            _machine.Fire(Trigger.Reject);
        }

        public void ProblemSolved()
        {
            _machine.Fire(Trigger.ProblemSolved);
        }

        public void ProblemNotSolved()
        {
            _machine.Fire(Trigger.ProblemNotSolved);
        }

        public void NeedInfo()
        {
            _machine.Fire(Trigger.NeedInfo);
        }

        public void OtherProduct()
        {
            _machine.Fire(Trigger.OtherProduct);
        }

        public void NeedDecisionLater()
        {
            _machine.Fire(Trigger.NeedDecisionLater);
        }

        public void Return()
        {
            _machine.Fire(Trigger.Return);
        }

        public void ReturnToAnalysis()
        {
            _machine.Fire(Trigger.ReturnToAnalysis);
        }

        public void Reopen()
        {
            _machine.Fire(Trigger.Reopen);
        }

        public void NoTimeNow()
        {
            _machine.Fire(Trigger.NoTimeNow);
        }

        public void NeedSeparateSolution()
        {
            _machine.Fire(Trigger.NeedSeparateSolution);
        }

        public void ProblemOtherProduct()
        {
            _machine.Fire(Trigger.ProblemOtherProduct);
        }

        public void NeedMoreInfo()
        {
            _machine.Fire(Trigger.NeedMoreInfo);
        }

        public void ConfirmOK()
        {
            _machine.Fire(Trigger.ConfirmOK);
        }

        public void ConfirmNotOK()
        {
            _machine.Fire(Trigger.ConfirmNotOK);
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация WorkFlow работы с багом ===\n");

            var bug = new Bug();
            Console.WriteLine($"Начальное состояние: {bug.CurrentState}\n");

            bug.StartAnalysis();
            bug.StartFixing();
            bug.ProblemSolved();
            Console.WriteLine($"\nФинальное состояние: {bug.CurrentState}\n");

            Console.WriteLine("=== Демонстрация завершена ===");
        }
    }
}
