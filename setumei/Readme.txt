SrkMdlxBuilder:
Importing a model to mdlx will not be a piece of cake. Your DAE model must have a very basic structure.
It must have only ONE root bone, and the bones must be of type "JOINT". If not, the program will not work.
My tool does not always interpretate the matrices of the DAE skeleton correctly.
In that happends, you can just tell the tool to reuse an already existing and correct MDLX skeleton.
If you want the tool to avoid creating the skeleton from the DAE and reuse a official skeleton from a official MDLX,
just put the mdlx file near to the dae file to convert, and give it the same name.