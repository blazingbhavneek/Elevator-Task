   M S B A M L   `   `   `  ����C  ?Elevator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ElevatorElevator  5   	       Elevator.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Elevator     5   	   -   5          �Name$
  Main��5   	    � Title$$ Bhavneek's Elevator Simulation��5	   	   $
��Auto��60    � WindowStartupLocation$ CenterScreen=�6>    � 	Resources 5
       �� 
TargetType �� Property!  System.Windows.SystemColors   ControlTextBrushKey �� Value �� 
TargetType	 �� CornerRadius
 �� Padding J� SnapsToDevicePixels �� Triggers P� Property P� Value   GrayTextBrushKey��%@  '��       �� 5   
   " ��6   .�5      ��  � ��# �  6+   �� 5       � ��
 ���� 6+   �� 5       � ��$ 5q�6(   �� 5       � ��$
 Left=�6;   �� 5       � ��$	 Top=�69   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $	 40��6!   #��z��63   #��z��6]   #��z��6�   #
 z��6�   $
 true��6�   ._�5      �� #��z��6/   $
��True��6r   # z 6�   #��z��6�   5       5      P� 5       � {�$ false��6;   . �5   "   �� # �  6?    � ��5      5      5      5      5       .��5"      � .+�5#   
   �� $
��Auto=�6   $
��Auto=�6:   .��5$      �� .+�5%      �� -   5         ��Name$ flexGrid��5%       �� Orientation$ 
Horizontal=�6/   $��Center=�6H   5&      �� 5'      -   5         ��Name$ ConsoleTextBox��5'      " �� VerticalScrollBarVisibility$
 Auto=�5(      $ �� HorizontalScrollBarVisibility$
 Auto=�5)       �� 
IsReadOnly$
 True��5*       �� TextWrapping$
 Wrap=�5+       �� AcceptsReturn$
 True��5,      $��10q�5-      $��Stretch=�5.      $	��100��5/      
���   �
�������51      53      54      55      56      