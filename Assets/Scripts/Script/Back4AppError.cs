using System;
using System.Diagnostics;
using UnityEngine;

public static class Back4AppError
{
    public static string GetErrorMessage(int code) => code switch
    {
        //APi Issue
        101 => "Invalid login parameters",
        102 => "There is a problem with the parameters used to construct this query",
        105 => "An invalid field name. Keys are case-sensitive. They must start with a letter",
        107 => "Badly formed JSON was received upstream",
        109 => "You must call Parse.initialize before using the Parse library",
        116 => "The object is too large. Parse Objects have a max size of 128 kilobytes",
        117 => "An invalid value was set for the limit",
        118 => "An invalid value was set for skip",
        119 => "The operation isn’t allowed for clients due to class-level permissions",
        120 => "The result was not found in the cache.",
        121 => "An invalid key was used in a nested JSONObject",
        123 => "An invalid ACL was provided",
        125 => "The email address was invalid",
        137 => "Unique field was given a value that is already taken",
        139 => "Role’s name is invalid",
        140 => "You have reached the quota on the number of classes in your app",
        141 => "Cloud Code script failed",
        142 => "Cloud Code validation failed",
        143 => "Webhook error",
        150 => "Invalid image data",
        151 => "An unsaved file",
        152 => "An invalid push time was specified",
        158 => "Hosting error",
        160 => "The provided analytics event name is invalid.",
        255 => "Class is not empty and cannot be dropped",
        256 => "App name is invalid",
        902 => "The request is missing an API key",
        903 => "The request is using an invalid API key",
        //user related error
        200 => "Invalid login parameters",
        201 => "The specified object or session doesnt exist or could not be found | Password missing",
        202 => "Invalid field name or an invalid field type for a specific constraint",
        203 => "An invalid field name",
        204 => "Badly formed JSON was received upstream",
        205 => "User WithEmail Not Found",
        206 => "A user object without a valid session could not be altered",
        207 => "A user can only be created through signup",
        208 => "An account being linked is already linked to another user",
        209 => "The device’s session token is no longer valid",
        //general Issues
        -1 => "Invalid login parameters",
         1 => "The specified object or session doesnt exist or could not be found",
         2 => "There is a problem with the parameters used to construct this query",
         4 => "An invalid field name. Keys are case-sensitive",
        _ => "Exception not found"
    };
}