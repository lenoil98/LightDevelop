﻿Imports System.Runtime.CompilerServices

Namespace Extension
	Public Module Extensions		
		<Extension()> _
		Public Function Multiply(pt As Point, factor As Double) As Point
			Return New Point(pt.X * factor, pt.Y * factor)
		End Function
	
		<Extension()> _
		Public Function Minus(pt As Point, p2 As Point) As Point
			Return New Point(pt.X - p2.X, pt.Y - p2.Y)
		End Function
	
		<Extension()> _
		Public Function Add(pt As Point, p2 As Point) As Point
			Return New Point(pt.X + p2.X, pt.Y + p2.Y)
		End Function
	
		<Extension()> _
		Public Function Plus(pt As Point, p2 As Size) As Point
			Return New Point(pt.X + p2.Width, pt.Y + p2.Height)
		End Function
	
		<Extension()> _
		Public Function Multiply(c As Color, factor As Double, Optional withAlpha As Boolean = False) As Color
			Return color.FromArgb(CInt(IIf(withAlpha, c.A * factor, c.A)), 
								  c.R * factor, c.G * factor, c.B * factor)
		End Function
	End Module
End Namespace
