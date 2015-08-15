using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cms
{
    public class Lang
    {
        //Menu
        public static string menu_home = "Admin Home";
        public static string menu_menu = "Menu";        
        public static string menu_news = "news";

        //Menu menu
        public static string menu_index_header = "Menu List";
        public static string menu_add_header = "Add New Menu";
        public static string menu_edit_header = "Update Menu";
        public static string menu_add_header_notice = "* is required field";
        public static string menu_add_header_add_root = "Add Root Menu";
        public static string menu_add_header_add_sub = "Add Sub Menu";
        public static string menu_add_header_edit = "Edit";
        public static string menu_add_header_delete = "Delete";
        public static string menu_field_name = "Name *";
        public static string menu_field_des = "Description";
        public static string menu_field_urlkey = "Url key";
        public static string menu_field_isactive = "Is Active *";
        public static string menu_field_inmenu = "Include in Navigation Menu *";
        public static string menu_field_parent_menu = "Belong Parent Menu *";
        public static string menu_field_order_no = "Order No";
        public static string menu_root_name = "Root Menu";
        public static string menu_field_type = "Type";

        ////Alert
        public static string alert_save_success = "Saved";
        public static string alert_save_not_success = "Can not save, some errors occur!";
        public static string alert_delete_not_success = "Can not delete, some errors occur!";
        public static string alert_move_not_success = "Can not move now, some errors occur!";
        public static string alert_input_name_field = "type name!";
        public static string alert_edit_item = "choose item for edit!";

    }
}