   M S B A M L   `   `   `  ����B  >CSE3055, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CSE3055CSE3055  5   	       CSE3055.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:CSE3055     5   	     � Title$  Login��5   	   $	��450��6   $	��800��6$   .��5	      � .+�5
   
   �� $?��9C:\Users\faruk\source\repos\CSE3055\Images\Background.png��6    J� Opacity$	 0.8Q�6S   � 5   
   $��62,0,480.6,187q�6   
�������$	��149��6:   $��Bottom=�6G   $	 0.8Q�6b    � RowDefinitions 5      �� 5      $
��252* �6    .+�5      �� -   5         ��Name$ errormessage��5   Q   $��20��6   $
��Left=�6$   $��67,0,0,0q�6?   $	��Top=�6h   $	��247��6�    J� OpacityMask
 �<�����	#FFE5572C6�   �� 5      -   5        $��textBlockUsername��5   R   $��23��6   $
��Left=�6$   $��	10,10,0,0q�6?   $��	Username:��6k   $	��Top=�6|   $	��110��6�   $��12(�6�   $��Impact)�6�   �� 5      -   5        $��textBlockPassword��5   R   $��23��6   $
��Left=�6$   $��	10,38,0,0q�6?   $��	Password:��6k   $	��Top=�6|   $	��110��6�   $��Impact)�6�   �� 5      -   5        $��textBoxUsername��5   Q   $��23��6   $
��Left=�6"   $��
102,10,0,0q�6=   $	��Top=�6h   $	��133��6�   G� 5      -   5        $��passwordBox��5   U   $��23��6   $
��Left=�6&   $��
102,38,0,0q�6A   $	��Top=�6h   $	��133��6�   �� 5      -   5        $��loginButton��5   `   $��Login��6   $��23��6%   $
��Left=�61   $��
135,76,0,0q�6L   $	��Top=�6s   $��70��6�    J� RenderTransformOrigin$ 0.483,-2.313&�6�   ���	#FFF10D0D
���   �
���   �$��Impact)�6  �� 5      -   5         ��Name$ loginButton_Copy��5   d   $��Register��6   $��23��6(   $
��Left=�64   $��135,116,0,0q�6O   $	��Top=�6~   $��70��6�   $ 0.483,-2.313&�6�   ���	#FF423DFF
���   �
���   �$��Impact)�6  5      5      5      