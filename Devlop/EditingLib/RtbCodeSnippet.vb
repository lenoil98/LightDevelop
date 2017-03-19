﻿Imports System.Text.RegularExpressions

Public Class CodeSnippetSup
	Inherits EditingSupporter

	Public WithEvents SnippetList As ListBox
	Dim snippets As New Dictionary(Of String, String)

	Public Sub New(box As RichTextBox, list As ListBox)
		MyBase.New(box)
		SnippetList = list
		
		Read()
	End Sub

	Private Sub Read()
		If Settings.EnableCodeSnippets Then
			Try
				Dim x = Xml.XmlReader.Create(
						Settings.CodeSnippetsLocation, New Xml.XmlReaderSettings() With {.ProhibitDtd = False})
				x.ReadToFollowing("Item")
				Do
					Dim snipTitle, snipText As String
					snipTitle = x.GetAttribute("name")
					snipText = Regex.Replace(x.ReadString(), "\s{2,}", "")
					snippets.Add(snipTitle, snipText)
				Loop While x.ReadToNextSibling("Item")
				x.Close()

				SnippetList.Items.Clear()
				For Each pair In snippets
					SnippetList.Items.Add(pair.Key)
				Next
			Catch ex As Exception
				MessageBox.Show("Bad snippets file format! Disable code snippets to avoid this message.",
								"Bad format", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End Try
		End If
	End Sub

	Private Sub ListClick(sender As Object, e As EventArgs) Handles SnippetList.DoubleClick
		If SnippetList.SelectedIndices.Count = 1 Then
			Dim line As Integer = EditTextBox.GetLineFromCharIndex(EditTextBox.SelectionStart)
            Dim oldSelectionStart = EditTextBox.SelectionStart
            If line = -1 Then Exit Sub
            Dim spaces As Integer = EditTextBox.Lines(line).Length - LTrim(EditTextBox.Lines(line)).Length

			Dim snip As String = snippets(SnippetList.SelectedItem).Replace("\n", vbCrLf & StrDup(spaces, " "))
			EditTextBox.Text = 
                EditTextBox.Text.Insert(oldSelectionStart, snip)

			EditTextBox.SelectionStart = oldSelectionStart
		End If
	End Sub
End Class
