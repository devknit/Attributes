
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Attributes.Editor
{
	public static class EditorGUIHelper
	{
		public static float GetIndentLength( Rect sourceRect)
		{
			return EditorGUI.IndentedRect( sourceRect).x - sourceRect.x;
		}
		public static void Line( Rect position, float thickness, Color color)
		{
			position.height = thickness;
			EditorGUI.DrawRect( position, color);
		}
		public static void HelpBox( Rect position, string message, MessageType type, UnityEngine.Object context=null, bool logToConsole=false)
		{
			EditorGUI.HelpBox( position, message, type);

			if( logToConsole != false)
			{
				DebugLogMessage( message, type, context);
			}
		}
		public static void HelpBoxLayout( string message, MessageType type, UnityEngine.Object context=null, bool logToConsole=false)
		{
			EditorGUILayout.HelpBox( message, type);

			if( logToConsole != false)
			{
				DebugLogMessage( message, type, context);
			}
		}
		static void DebugLogMessage( string message, MessageType type, UnityEngine.Object context)
		{
			switch( type)
			{
				case MessageType.None:
				case MessageType.Info:
				{
					Debug.Log( message, context);
					break;
				}
				case MessageType.Warning:
				{
					Debug.LogWarning( message, context);
					break;
				}
				case MessageType.Error:
				{
					Debug.LogError( message, context);
					break;
				}
			}
		}
	}
}
