using System.Diagnostics;

namespace TurtleEngine;

/// <summary>
///     Defines methods to ensure the state of conditions or object during debugging.
/// </summary>
public static class Ensure
{
    /// <summary>
    ///     Ensures that a fail state occurs and signals a breakpoint to the attached debugger.
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void Fail()
    {
        Debug.Assert(false);
        Debugger.Break();
    }

    /// <summary>
    ///     Ensures that a fail state occurs, outputs the specified message in a message box, and signals a breakpoint
    ///     to the attached debugger.
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="message">
    ///     The message to display in the message box.
    /// </param>
    /// <param name="args">
    ///     Arguments to supply to as format items to the <paramref name="message"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void Fail(string message, params object[] args)
    {
        Debug.Assert(false, string.Format(message, args));
        Debugger.Break();
    }

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <paramref name="condition"/> is <see langword="true"/>. 
    ///     </para>
    ///     <para>
    ///         When <paramref name="condition"/> is <see langword="false"/>, ensures a fail state is triggered and 
    ///         signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="condition">
    ///     The condition to ensure is <see langword="true"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsTrue(bool condition)
    {
        if (!condition) Fail();
    }

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <paramref name="condition"/> is <see langword="true"/>. 
    ///     </para>
    ///     <para>
    ///         When <paramref name="condition"/> is <see langword="false"/>, ensures a fail state is triggered,
    ///         outputs the specified message in a message box, and signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="condition">
    ///     The condition to ensure is <see langword="true"/>.
    /// </param>
    /// <param name="message">
    ///     The message to display in the message box.
    /// </param>
    /// <param name="args">
    ///     Arguments to supply to as format items to the <paramref name="message"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsTrue(bool condition, string message, params object[] args)
    {
        if (!condition) Fail(message, args);
    }

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <paramref name="condition"/> is <see langword="false"/>. 
    ///     </para>
    ///     <para>
    ///         When <paramref name="condition"/> is <see langword="true"/>, ensures a fail state is triggered and 
    ///         signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="condition">
    ///     The condition to ensure is <see langword="false"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsFalse(bool condition) => IsTrue(!condition);

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <paramref name="condition"/> is <see langword="false"/>. 
    ///     </para>
    ///     <para>
    ///         When <paramref name="condition"/> is <see langword="true"/>, ensures a fail state is triggered,
    ///         outputs the specified message in a message box, and signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="condition">
    ///     The condition to ensure is <see langword="false"/>.
    /// </param>
    /// <param name="message">
    ///     The message to display in the message box.
    /// </param>
    /// <param name="args">
    ///     Arguments to supply to as format items to the <paramref name="message"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsFalse(bool condition, string message, params object[] args) => IsTrue(!condition, message, args);

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <see cref="object"/> is <see langword="null"/>. 
    ///     </para>
    ///     <para>
    ///         When the specified <see cref="object"/> is not <see langword="null"/>, ensures a fail state is triggered
    ///         and signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="object">
    ///     The <see cref="object"/> to ensure is <see langword="null"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsNull(object obj) => IsTrue(obj is null);

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <see cref="object"/> is <see langword="null"/>. 
    ///     </para>
    ///     <para>
    ///         When the specified <see cref="object"/> is not <see langword="null"/>, ensures a fail state is 
    ///         triggered, outputs the specified message in a message box, and signals a breakpoint to the attached 
    ///         debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="object">
    ///     The <see cref="object"/> to ensure is <see langword="null"/>.
    /// </param>
    /// <param name="message">
    ///     The message to display in the message box.
    /// </param>
    /// <param name="args">
    ///     Arguments to supply to as format items to the <paramref name="message"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void IsNull(object obj, string message, params object[] args) => IsTrue(obj is null, message, args);

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <see cref="object"/> is not <see langword="null"/>. 
    ///     </para>
    ///     <para>
    ///         When the specified <see cref="object"/> is <see langword="null"/>, ensures a fail state is triggered
    ///         and signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="object">
    ///     The <see cref="object"/> to ensure is <see langword="null"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void NotNull(object obj) => IsTrue(obj is not null);

    /// <summary>
    ///     <para>
    ///         Ensure that the specified <see cref="object"/> is not <see langword="null"/>. 
    ///     </para>
    ///     <para>
    ///         When the specified <see cref="object"/> is <see langword="null"/>, ensures a fail state is triggered, 
    ///         outputs the specified message in a message box, and signals a breakpoint to the attached debugger.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Will only be called when DEBUG directive is defined.
    /// </remarks>
    /// <param name="object">
    ///     The <see cref="object"/> to ensure is not <see langword="null"/>.
    /// </param>
    /// <param name="message">
    ///     The message to display in the message box.
    /// </param>
    /// <param name="args">
    ///     Arguments to supply to as format items to the <paramref name="message"/>.
    /// </param>
    [Conditional("DEBUG")]
    [DebuggerHidden]
    public static void NotNull(object obj, string message, params object[] args) => IsTrue(obj is not null, message, args);
}