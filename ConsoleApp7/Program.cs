// паттерн Состояние (State)
// поведенческий паттерн
// данный паттерн позволяет организовать
// структуру, при которой поведение объекта
// будет зависеть от его переменного состояния
// т.е. можно вынести состояние из класса в ряд
// других классов, которые будут содержать
// другое поведение

using System.Drawing;

// пример без паттерна
/*class Board
{ 
    public Color ColorTheme { get; set; }

    public bool IsTextSeenGood()
    {
        if (ColorTheme == Color.White)
        {
            Console.WriteLine("На доске нифига не видно!");
            return false;
        }
        else if (ColorTheme == Color.Black)
        {
            Console.WriteLine("На доске видно чуть больше, чем нифига!");
            return true;
        }
        return false;
    }
}
*/
/*
//пример с паттерном
Board board = new Board();
//board.SetCurrentState(new WhiteBoardState());
Console.WriteLine(board.IsTextSeenGood());
//board.SetCurrentState(new BlackBoardState());
Console.WriteLine(board.IsTextSeenGood());

abstract class State
{
    public Color ColorTheme { get; set; }
    public abstract bool IsTextSeenGood();
    public abstract State ChangeState();
}

class WhiteBoardState : State
{
    public override State ChangeState()
    {
        return new BlackBoardState();
    }

    public override bool IsTextSeenGood()
    {
        Console.WriteLine("На доске нифига не видно!");
        return false;
    }
}

class BlackBoardState : State
{
    public override State ChangeState()
    {
        return new WhiteBoardState();
    }

    public override bool IsTextSeenGood()
    {
        Console.WriteLine("На доске видно чуть больше, чем нифига!");
        return true;
    }
}

class Board
{
    State state;

    public Board()
    {
        state = new BlackBoardState();
    }
    // вместо публичного метода, как со стратегией
    // состояние может переключать внутри класса
    // самостоятельно
    public void SetCurrentState(State state)
    {
        this.state = state;
    }

    public bool IsTextSeenGood()
    { 
        var result = state.IsTextSeenGood();
        state = state.ChangeState();
        return result;
    }
}
*/

// вариант, отличающийся от стратегии хотя бы
// тем, что состояние внутри студента переключается
// само
Student student = new Student();
for(int i = 0; i < 100; i++)
    student.Work();

abstract class StateStudent
{
    internal abstract void Work();
    internal abstract StateStudent GetNextStage();
}

internal class StateSomeTiredStudent : StateStudent
{
    internal override StateStudent GetNextStage()
    {
        return new StateTiredStudent();
    }

    internal override void Work()
    {
        Console.WriteLine("Сейчас немножко отдохну и все сделаю..");
    }
}

internal class StateTiredStudent : StateStudent
{
    internal override StateStudent GetNextStage()
    {
        return new StateDrunkStudent();
    }

    internal override void Work()
    {
        Console.WriteLine("Сегодня не смогу. Попробую завтра. Но не обещаю.");
    }
}

internal class StateDrunkStudent : StateStudent
{
    internal override StateStudent GetNextStage()
    {
        return new StateTiredAndDrunkStudent();
    }

    internal override void Work()
    {
        Console.WriteLine(".............");
    }
}

internal class StateTiredAndDrunkStudent : StateStudent
{
    internal override StateStudent GetNextStage()
    {
        return new StateStudentReadyToWork();
    }

    internal override void Work()
    {
        Console.WriteLine("Ща.. ща.. хрр... ща");
    }
}

class StateStudentReadyToWork : StateStudent
{
    internal override StateStudent GetNextStage()
    {
        return new StateSomeTiredStudent();
    }

    internal override void Work()
    {
        Console.WriteLine("Сейчас все сделаю! Вообще без Б!");
    }
}


class Student
{
    StateStudent state;

    public Student()
    {
        state = new StateStudentReadyToWork();
    }

    public void Work()
    {
        state.Work();
        // меняем состояние при работе!
        state = state.GetNextStage();
    }
}