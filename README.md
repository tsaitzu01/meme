## meme
This is a **Meme Generator** developed by Visual basic

## How to use this generator?
* Download __.exe__.
> If firebase database still work, then you can preview and download other's meme, besides, you can make your own meme.
> If firebase is close, then the error message would be shown. However, you can create a firebase realtime database to make this project work.

## How to connect firebase and visual basic?
First of all, you need to download package to make this project work.
1. Go to Tool bar: `Project > Management NuGet package`
2. Please download following package:
   * **FireSharp2.0.4**: This is for connect firebase and Visual basic
   * **ILMerge.3.0.29**: This is for include package in .exe, if you would like to send exe file to others but not all project, then you would need this tool.
Now, modify some codes to connect your firebase and your vb project!
1. Add Auth Secret and project URL in **`OpenPict.vb, Preview.vb, MainForm.vb, EditPic.vb`**
```
'-------------------------------------------Configure FireSharp
    Public fcon As New FirebaseConfig() With
        {
        .AuthSecret = "(Your Firebase project Auth Secret)",
        .BasePath = "(Your Firebase project URL)"
        }
    Public client As IFirebaseClient
    '-------------------------------------------
```
2. Add Try Catch code in Page_Load function in above page
```
  Try
    client = New FireSharp.FirebaseClient(fcon)
  Catch ex As Exception
    MessageBox.Show(ex.Message)
  End Try
```
