   M S B A M L   `   `   `  ����F  BWpfSchedule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfScheduleWpfSchedule  5      '    !WpfSchedule.DayDescriptionControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:WpfSchedule     5        � 	Resources  5
       �� 
TargetType   
dropShadow �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   Color   Opacity   RenderingBias   ShadowDepth��%�   &	        �� 5   
   " ��6   .�5      ��  �@J�Effect 5        5      $ 10Q�5      $ 	#FF3C3C3C��5      $	 0.7Q�5      $ Quality=�5      $ 7Q�5      5      5      5      .��5      �� -   5        $��
mainBorder��5      
�������._�5   
   � 	 � RowDefinitions	 5   
   �� 5       $��60 �6   �� 5!      $��* �6   .+�5#   
   �� $��0��6   
��� ���._�5$      � 
 � ColumnDefinitions
 5%      �� 5&      $	��60* �6'   �� 5'      $	��40* �6'   .+�5)      �� -   5        $��labelDayOfWeek��5)      $��0��5*      $��Center=�5+      $
��Left=�5,      $��20(�5-      $
��Bold"�5.      
�������.��5/   +   понедельник51      �� 52      -   5        $��	labelDate��52      $��1��53      $��Center=�54      $��Right=�55      $��20(�56      
���   �.��57   +   
13.02.201759      5:      5;      �� 5<   
   $��1��6   
�������._�5>      �� -   5        $��panelLessons��5>       �� Orientation$ Vertical=�5?      .+�5@      *   $WpfSchedule.LessonDescriptionControl  $��4q�61     5A      $��4q�61     5B      $��4q�61   5C      5D      5E      5F      5G      