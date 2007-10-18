dim fs
set fs = createobject("Scripting.FileSystemObject")

dim folder
dim file

dim subfolder

checkfolder(".\")

msgbox "Finished.", vbinformation , "License Checker"

sub checkfolder(parentfolder)
	on error resume next
	dim folder
	set folder = fs.GetFolder(parentfolder)

	for each subfolder in folder.subfolders
	    if instr(1, subfolder, "test", vbtextcompare) = 0 and instr(1, subfolder, "tmp", vbtextcompare) = 0 then
		    checkfolder(subfolder)
		end if
	next
	
	for each file in folder.files
		'msgbox file.name
		If testend(file.name, ".vb") and testend(file.name, ".Designer.vb") = false then
			dim contents
			dim txtfile
			'msgbox file.patH & vbtab & file.parentfolder
			set txtfile = fs.OpenTextFile(file.path)', ForReading)
			if err.number > 0 then
				msgbox err.Description 
				msgbox "The file was: '" & file.path & "'"
				exit sub
			end if
			contents = txtfile.readall
			if instr(1,contents, "GNU General Public License") > 0 then
				msgbox "File '" & file.path & "' has an old license."
			elseif instr(1, contents, "GNU Lesser General Public") = 0 then
			    msgbox "File '" & file.path & "' does not have a license."
			end if
			txtfile.close
		end if
	next
end sub

function testend(str , str2) 
    dim length 
    length = len(str2)
    testend = right(str, length) = str2
end function