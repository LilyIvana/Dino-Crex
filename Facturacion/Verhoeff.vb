''' <summary>
''' For more information cf. http://en.wikipedia.org/wiki/Verhoeff_algorithm
''' Dihedral Group: http://mathworld.wolfram.com/DihedralGroup.html
''' </summary>
''' <remarks></remarks>
Public Class Verhoeff

    'The multiplication table
    Shared ReadOnly d(,) As Integer =
    {{0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
    {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
    {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
    {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
    {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
    {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
    {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
    {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
    {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
    {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}}

    'The permutation table
    Shared ReadOnly p(,) As Integer =
    {{0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
    {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
    {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
    {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
    {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
    {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
    {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
    {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}}

    'The inverse table
    Shared ReadOnly inv() As Integer = {0, 4, 3, 2, 1, 5, 6, 7, 8, 9}

    ''' <summary>
    ''' Validates that an entered number is Verhoeff compliant.
    ''' </summary>
    ''' <param name="num"></param>
    ''' <returns>True if Verhoeff compliant, otherwise false</returns>
    ''' <remarks>Make sure the check digit is the last one!</remarks>
    Public Shared Function validateVerhoeff(ByVal num As String) As Boolean

        Dim c As Integer = 0
        Dim myArray() As String = StringToReversedIntArray(num)

        For i As Integer = 0 To myArray.Length - 1
            c = d(c, p((i Mod 8), myArray(i)))
        Next i

        Return c.Equals(0)

    End Function

    ''' <summary>
    ''' For a given number generates a Verhoeff digit
    ''' </summary>
    ''' <param name="num"></param>
    ''' <returns>Verhoeff check digit as string</returns>
    ''' <remarks>Append this check digit to num</remarks>
    Public Shared Function generateVerhoeff(ByVal num As String) As String

        Dim c As Integer = 0
        Dim myArray() As String = StringToReversedIntArray(num)

        For i As Integer = 0 To myArray.Length - 1
            c = d(c, p(((i + 1) Mod 8), Convert.ToInt32(myArray(i))))
        Next i

        Return inv(c).ToString

    End Function

    ''' <summary>
    ''' Converts a string to a reversed integer array.
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns>Reversed integer array</returns>
    ''' <remarks></remarks>
    Private Shared Function StringToReversedIntArray(ByVal str As String) As String()

        Dim myArray(str.Length - 1) As String
        For i As Integer = 0 To str.Length - 1
            myArray(i) = str.Substring(i, 1)
        Next
        Array.Reverse(myArray)
        Return myArray

    End Function
End Class
