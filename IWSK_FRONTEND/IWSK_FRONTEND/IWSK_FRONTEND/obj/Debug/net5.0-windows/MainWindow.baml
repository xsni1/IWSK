   M S B A M L   `   `   `  ����H  DIWSK_FRONTEND, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:IWSK_FRONTENDIWSK_FRONTEND  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   E AIWSK_RS232, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:IWSK_RS232;assembly=IWSK_RS232
IWSK_RS232 5   	   U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 6D   H DWPFHexaEditor, Version=2.1.7.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:WpfHexaEditor;assembly=WPFHexaEditorWpfHexaEditor 5	   	   1&clr-namespace:System;assembly=mscorlibSystem 5
   	   1&clr-namespace:System;assembly=mscorlibSystem 5   	       IWSK_FRONTEND.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R	 NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  	   5   	   @d2http://schemas.microsoft.com/expression/blend/2008  	   5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  	   5   	   +enmclr-namespace:IWSK_FRONTEND  	   5   	   9System&clr-namespace:System;assembly=mscorlib  	   5   	   >local,clr-namespace:IWSK_RS232;assembly=IWSK_RS232  	   5   	   C
extensions,clr-namespace:System;assembly=System.Runtime  	   6D   Fcontrol2clr-namespace:WpfHexaEditor;assembly=WPFHexaEditor  	   5	   	   6sys&clr-namespace:System;assembly=mscorlib  	   5
   	   8codeg&clr-namespace:System;assembly=mscorlib  	   5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5       S� 
MethodName S� 
ObjectType  System.Enum   ParityTypes S� MethodParameters M� Type  IWSK_RS232.ParityTypes  TerminatorTypes   IWSK_RS232.TerminatorTypes  FlowControlTypes!  IWSK_RS232.FlowControlTypes��%>  &	        &	 `     &	 �     S� 5   
   $ 	GetValues��6   "  64    5      M� 5      "  6   64   5      S� 5   
   $ 	GetValues��6   "  64    5      M� 5      "  6   68   5      S� 5   
   $ 	GetValues��6   "  64    5      M� 5      "  6   69   5       5!      .��5#      � -   5         �Name$ myGrid_Copy��5#       � ColumnDefinitions 5$   
   �� 5%      $	��41* �6   �� 5&      $	��26* �6   �� 5'      $
��333* �6   .+�5)   
   �� $��Connect��6   $
��Left=�6#   $��
38,386,0,0q�6>   $	��Top=�6R   $��3��6�   $	��119��6�   ��6j   �� *ClickCommand+6�   �� 5*   
   -   5         ��Name$ messageInput1��5*   y   $
��Left=�6   $��
134,33,0,0q�6-   	 �� TextWrapping$
	 Wrap=�6A   $	��Top=�6U   $	��132��6m   $��2��6�   $��32��6�   ��6�   �� *MessageInput+6�   �� 5+   
   $
��Left=�65   $��
412,35,0,0q�6P   $
��Wrap=�6d   $	��Top=�6x   $	��171��6�   $��2��6�   ��6   �� *ReceivedMessage+�� 6�   
 � Language$
 pl-pl�6�   6�   �� 5,   
   $
��Left=�6f   $��	41,29,0,0q�6�   $	��Top=�6�   $	��120��6�   $��3��6�   ��6   �� *SerialPortNames+ �� SelectedValue 6;   �� *SelectedPortName+6�   �� 5-   
   $
��Left=�6V   $��	41,82,0,0q�6q   $	��Top=�6�   $	��120��6�   $��3��6�   ��6   �� *
BitRates+ 64   �� *	BitRate+6�   �� 5.   
   $
��Left=�6V   $��
44,132,0,0q�6q   $	��Top=�6�   $	��120��6�   $��3��6�   ��6   �� *
DataBits+ 64   �� *	DataBit+6�   �� 50   
   $
��Left=�6t   $��
44,230,0,0q�6�   $	��Top=�6�   $	��120��6�   $��3��6�   ��6   ��  �� Source# [   6O   �� *
ParityType+6�   �� 52   
   $
��Left=�6V   $��
44,181,0,0q�6q   $	��Top=�6�   $	��120��6�   $��3��6�   ��6   �� *
StopBits+ 64   �� *	StopBit+6�   �� 53   
   $��Port:��6   $
��Left=�6    $��	38,10,0,0q�6;   $	��Top=�6N   �� 54   
   $��	Bitrate: ��6   $
��Left=�6$   $��	38,64,0,0q�6?   $	��Top=�6R   $��2��6j   $��54��6~   �� 55   
   $��
Data bits:��6   $
��Left=�6%   $��
41,114,0,0q�6@   $	��Top=�6T   $��2��6l   $��63��6�   �� 56   
   $��Message:��6   $
��Left=�6#   $��
134,14,0,0q�6>   $	��Top=�6R   $��63��6j   $��2��6u   �� 57   
   $��Parity type:��6   $
��Left=�6'   $��
41,212,0,0q�6B   $	��Top=�6V   $��2��6n   $��79��6�   �� 58   
   $��
Stop bits:��6   $
��Left=�6%   $��
41,163,0,0q�6@   $	��Top=�6T   $��2��6l   $��79��6�   �� 59   
   $
��Send��6   $
��Left=�6    $��
134,78,0,0q�6;   $	��Top=�6O   $	��132��6�   $��2��6�   ��6g   �� *SendCommand+6�   �� 5:   
   $��2��6z   $
��Left=�6�   $��134,181,0,0q�6�   $	��Top=�6�   $	��120��6�   ��6   �� # [  6T   �� *FlowControl+6�   �� 5<   
   $��Flow control:��6   $��2��6(   $
��Left=�68   $��134,163,0,0q�6S   $	��Top=�6h   �� 5=   
   $	��DTR��6   $��2��6!   $
��Left=�61   $��134,218,0,0q�6L   $	��Top=�6a    d� 	IsChecked 6y   �� *IsDTRActive+{�6�   �� *CanControlSignals+�� 5>   
   $	��DSR��6   $��2��6!   $
��Left=�61   $��134,238,0,0q�6L   $	��Top=�6a   ${�False��6�    6y   �� *IsDSRActive+6�   �� 5?   
   $	��RTS��6   $��2��6!   $
��Left=�61   $��134,269,0,0q�6L   $	��Top=�6a    6y   �� *IsRTSActive+{�6�   �� *CanControlSignals+�� 5@   
   $	��CTS��6   $��2��6!   $
��Left=�61   $��134,291,0,0q�6L   $	��Top=�6a   ${�False��6�    6y   �� *IsCTSActive+6�   �� 5B   
   $��2��6|   $
��Left=�6�   $��283,181,0,0q�6�   $	��Top=�6�   $	��120��6�   ��6   �� # [  6S   �� *TerminatorType+6�   �� 5C   
   -   5        $ messageInput1_Copy��5C   �   $
��Left=�6;   $��283,230,0,0q�6V   $
	 Wrap=�6k   $	��Top=�6   $	��132��6�   $��2��6�   $��32��6�   {�6   �� *IsCustomTerminator+��6�   �� *
Terminator+6�   �� 5D   
   $��Terminator type:��6   $��2��6+   $
��Left=�6;   $��283,163,0,0q�6V   $	��Top=�6k   �� 5E   
   $��Custom terminator:��6   $��2��6-   $
��Left=�6=   $��283,212,0,0q�6X   $	��Top=�6m   �� 5F   
   -   5        $ messageInput1_Copy1��5F   y   $
��Left=�6   $��
44,299,0,0q�6-   $
	 Wrap=�6A   $	��Top=�6U   $	��132��6m   $��32��6�   $��3��6�   ��6�   �� *WriteTimeout+6�   �� 5G   
   $��Write timeout:��6   $
��Left=�6)   $��
41,280,0,0q�6D   $	��Top=�6X   $��3��6p   $��97��6�   �� 5H   
   $��Transaction��6   $
��Left=�6'   $��134,107,0,0q�6B   $	��Top=�6W   $	��132��6�   $��2��6�   ��6o   �� *TransactionCommand+6�   �� 5I   
   $
��Ping��6   $
��Left=�6    $��134,134,0,0q�6;   $	��Top=�6P   $	��132��6�   $��2��6�   ��6h   �� *PingCommand+6�   5V      5W      