'
' VBErrors.vb
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' 
'
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'
Friend Enum VBErrors
    ERR3_This_Error_number_is_obsolete_and_no_longer_used = 3
    ERR5_Invalid_procedure_call = 5
    ERR6_Overflow = 6
    ERR7_Out_of_memory
    ERR9_Subscript_out_of_range = 9
    ERR10_This_array_is_fixed_or_temporarily_locked = 10
    ERR11_Division_by_zero = 11
    ERR13_Type_mismatch = 13
    ERR14_Out_of_string_space = 14
    ERR16_Expression_too_complex = 16
    ERR17_Can_t_perform_requested_operation = 17
    ERR18_User_interrupt_occurred = 18
    ERR20_Resume_without_error = 20
    ERR28_Out_of_stack_space = 28
    ERR35_Sub_Function_or_Property_not_defined = 35
    ERR47_Too_many_DLL_application_clients = 47
    ERR48_Error_in_loading_DLL = 48
    ERR49_Bad_DLL_calling_convention = 49
    ERR51_Internal_error = 51
    ERR52_Bad_file_name_or_number = 52
    ERR53_File_not_found = 53
    ERR54_Bad_file_mode = 54
    ERR55_File_already_open = 55
    ERR57_Device_IO_error = 57
    ERR58_File_already_exists = 58
    ERR59_Bad_record_length = 59
    ERR61_Disk_full = 61
    ERR62_Input_past_end_of_file = 62
    ERR63_Bad_record_number = 63
    ERR67_Too_many_files = 67
    ERR68_Device_unavailable = 68
    ERR70_Permission_denied = 70
    ERR71_Disk_not_ready = 71
    ERR74_Can_t_rename_with_different_drive = 74
    ERR75_Path_File_access_error = 75
    ERR76_Path_not_found = 76
    ERR91_Object_variable_or_With_block_variable_not_set = 91
    ERR92_For_loop_not_initialized = 92
    ERR93_Invalid_pattern_string = 93
    ERR94_Invalid_use_of_Null = 94
    ERR95_Application_defined_or_object_defined_error = 95
    ERR97_Can_t_call_Friend_procedure_on_an_object_that_is_not_an_instance_of_the_defining_class = 97
    ERR98_A_property_or_method_call_cannot_include_a_reference_to_a_private_object_either_as_an_argument_or_as_a_return_value = 98
    ERR298_System_DLL_could_not_be_loaded = 298
    ERR320_Can_t_use_character_device_names_in_specified_file_names = 320
    ERR321_Invalid_file_format = 321
    ERR322_Cant_create_necessary_temporary_file = 322
    ERR325_Invalid_format_in_resource_file = 325
    ERR327_Data_value_named_not_found = 327
    ERR328_Illegal_parameter_cant_write_arrays = 328
    ERR335_Could_not_access_system_registry = 335
    ERR336_Component_not_correctly_registered = 336
    ERR337_Component_not_found = 337
    ERR338_Component_did_not_run_correctly = 338
    ERR360_Object_already_loaded = 360
    ERR361_Can_t_load_or_unload_this_object = 361
    ERR363_Control_specified_not_found = 363
    ERR364_Object_was_unloaded = 364
    ERR365_Unable_to_unload_within_this_context = 365
    ERR368_The_specified_file_is_out_of_date__This_program_requires_a_later_version = 368
    ERR371_The_specified_object_cant_be_used_as_an_owner_form_for_Show = 371
    ERR380_Invalid_property_value = 380
    ERR381_Invalid_property_array_index = 381
    ERR382_Property_Set_cant_be_executed_at_run_time = 382
    ERR383_Property_Set_cant_be_used_with_a_read_only_property = 383
    ERR385_Need_property_array_index = 385
    ERR387_Property_Set_not_permitted = 387
    ERR393_Property_Get_cant_be_executed_at_run_time = 393
    ERR394_Property_Get_cant_be_executed_on_write_only_property = 394
    ERR400_Form_already_displayed_cant_show_modally = 400
    ERR402_Code_must_close_topmost_modal_form_first = 402
    ERR419_Permission_to_use_object_denied = 419
    ERR422_Property_not_found = 422
    ERR423_Property_or_method_not_found = 423
    ERR424_Object_required = 424
    ERR425_Invalid_object_use = 425
    ERR429_Component_cant_create_object_or_return_reference_to_this_object = 429
    ERR430_Class_doesnt_support_Automation = 430
    ERR432_File_name_or_class_name_not_found_during_Automation_operation = 432
    ERR438_Object_doesnt_support_this_property_or_method = 438
    ERR440_Automation_error = 440
    ERR442_Connection_to_type_library_or_object_library_for_remote_process_has_been_lost = 442
    ERR443_Automation_object_doesnt_have_a_default_value = 443
    ERR445_Object_doesnt_support_this_action = 445
    ERR446_Object_doesnt_support_named_arguments = 446
    ERR447_Object_doesnt_support_current_locale_setting = 447
    ERR448_Named_argument_not_found = 448
    ERR449_Argument_not_optional_or_invalid_property_assignment = 449
    ERR450_Wrong_number_of_arguments_or_invalid_property_assignment = 450
    ERR451_Object_not_a_collection = 451
    ERR452_Invalid_ordinal = 452
    ERR453_Specified_not_found = 453
    ERR454_Code_resource_not_found = 454
    ERR455_Code_resource_lock_error = 455
    ERR457_This_key_is_already_associated_with_an_element_of_this_collection = 457
    ERR458_Variable_uses_a_type_not_supported_in_Visual_Basic = 458
    ERR459_This_component_doesnt_support_the_set_of_events = 459
    ERR460_Invalid_Clipboard_format = 460
    ERR461_Method_or_data_member_not_found = 461
    ERR462_The_remote_server_machine_does_not_exist_or_is_unavailable = 462
    ERR463_Class_not_registered_on_local_machine = 463
    ERR480_Can_t_create_AutoRedraw_image = 480
    ERR481_Invalid_picture = 481
    ERR482_Printer_error = 482
    ERR483_Printer_driver_does_not_support_specified_property = 483
    ERR484_Problem_getting_printer_information_from_the_system_Make_sure_the_printer_is_set_up_correctly = 484
    ERR485_Invalid_picture_type = 485
    ERR486_Can_t_print_form_image_to_this_type_of_printer = 486
    ERR520_Can_t_empty_Clipboard = 520
    ERR521_Can_t_open_Clipboard = 521
    ERR735_Can_t_save_file_to_TEMP_directory = 735
    ERR744_Search_tex_not_found = 744
    ERR746_Replacements_too_long = 746
    ERR31001_Out_of_memory = 31001
    ERR31004_No_object = 31004
    ERR31018_Class_is_not_set = 31018
    ERR31027_Unable_to_activate_object = 31027
    ERR31032_Unable_to_create_embedded_object = 31031
    ERR31036_Error_saving_to_file = 31036
    ERR31037_Error_loading_from_file = 31037

End Enum
