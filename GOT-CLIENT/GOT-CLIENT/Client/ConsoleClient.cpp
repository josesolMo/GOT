#include "ConsoleClient.h"


void run();

ConsoleClient* ConsoleClient::instance = 0;

ConsoleClient::ConsoleClient() {
	thrd = thread(run);
}

ConsoleClient* ConsoleClient::getInstance() {
	if (instance == 0) {
		instance = new ConsoleClient();
	}
	return instance;
}

void ConsoleClient::ConsoleComand() {
	string comand;
	cout << "Console Application" << endl;
	cout << "Por favor Ingrese Comando Disponible" << endl;
	cout << "-----------Comandos disponibles:-----------" << endl;
	cout << "* got init <name>" << endl;
	cout << "* got help" << endl;
	cout << "* got add <-A> / got add <name>" << endl;
	cout << "* got commit <mensaje>" << endl;
	cout << "* got status" << endl;
	cout << "* got rollback" << endl;
	cout << "* got reset" << endl;
	cout << "* got sync" << endl;
	cout << "-------------------------------------------" << endl;

	while (true) {
		cout << ">> ";
		getline(cin, comand);


		if (comand.find("got init") != string::npos) {
			string name = getInLine(comand);
			bool successful = _mkdir(name.c_str());

			if (successful) {
				repPath = name;
				cout << "La carpeta ya existe, ha ingresado a la siguiente carpeta " << name << endl;
				continue;
			}
			
			//REALIZA POST DEL REPOSITORIO Y OBTIENE EL ID DE ESTE
			string json = "{\"NameRepositorie\":\""+name+"\"}";

			std::cout << "Action: Post Archivo" << std::endl;
			auto r = cpr::Post(cpr::Url{ "https://localhost:44348/api/repositorie" },
				cpr::Body{ json },
				cpr::Header{ { "Content-Type", "application/json" } });

			struct json_object* tempjson;
			json_object* parsedjson = json_tokener_parse((r.text).c_str());
			json_object_object_get_ex(parsedjson, "idRepositorie", &tempjson);
			int repID = json_object_get_int(tempjson);
			cout << "Se ha creado la carpeta -> " + name + " <- exitosamente" << endl;


			//REALIZA POST DEL COMMIT Y OBTIENE EL ID DE ESTE
			string jsoncom = "{\"MessageCommit\":\"Agrega GotIgnore\",\"DateCommit\":\"2020-08-13T20:46:05.1468152-06:00\"}";

			auto c = cpr::Post(cpr::Url{ "https://localhost:44348/api/commited" },
				cpr::Body{ jsoncom },
				cpr::Header{ { "Content-Type", "application/json" } });

			struct json_object* tempjson2;
			struct json_object* tempjson3;
			json_object* parsedjson2 = json_tokener_parse((c.text).c_str());
			json_object_object_get_ex(parsedjson2, "idCommit", &tempjson2);
			json_object_object_get_ex(parsedjson2, "dateCommit", &tempjson3);
			int commitID = json_object_get_int(tempjson2);
			string date = json_object_get_string(tempjson3);

			std::cout << "Commit ID: " << commitID << std::endl;
			cout << "Fecha del commit: " << date << endl;
			cout << "COMMIT LISTO" << endl;
			

			//REALIZA POST DEL ARCHIVO Y LE ASIGNA ID DEL COMMIT Y DEL REPOSITORIO AL QUE PERTENECE
			string filedir = name+"/";
			cout << name << endl;
			string jsonfile = getFileJson(filedir,".gotignore.txt","",repID,commitID);

			cout << jsonfile << endl;
			auto a = cpr::Post(cpr::Url{ "https://localhost:44348/api/archivo" },
				cpr::Body{ jsonfile },
				cpr::Header{ { "Content-Type", "application/json" } });

			cout << "Se ha subido el commit con el archivo correspondiente" << endl;


			/*
			std::cout << "Obteniendo datos..." << std::endl;
			auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo" });
			cout<<f.text<<endl;
			*/

			ofstream file;
			file.open(name + "/.gotignore.txt");
			//file << "ignorarEjemplo";//se escribe lo que se quiere ignorar
			file.close();


			repPath = name;
			//repIdTemp = repID;
			continue;
		}


		if (comand == "got help") {
			cout << "______________________________| Help |_________________________________ " << endl;
			cout << "_______________________________________________________________________ " << endl;
			cout << endl;
			cout << "got init <name>:" "\n"
				"	Instancia un nuevo repositorio en el servidor y lo identifica"  "\n"
				"	con el nombre indicado por <name>. Ademas, crea cualquier estructura" "\n"
				"	de datos necesaria para llevar el control del lado del cliente" "\n"
				"	sobre cuales archivos estan siendo controlados por el servidor""\n"
				"	y cuales no. Debe crear un archivo llamado .gotignore que permite""\n"
				"	especificar cuales archivos son ignorados por el control de ""\n"
				"	versiones.El servidor almacena esta informacion tambien." << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got add [-A] [name]:" "\n"
				"	Permite agregar todos los archivos que no esten registrados""\n"
				"	o que tengan nuevos cambios al repositorio. Ignora los archivos""\n"
				"	que esten configurados en .gotignore. El usuario puede indicar""\n"
				"	cada archivo por agregar, o puede usar el flag -A para agregar""\n"
				"	todos los archivos relevantes. Cuando los archivos se agregan,""\n"
				"	se marcan como pendientes de commit." << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got commit <mensaje>:""\n"
				"	Envia los archivos agregados y pendientes de commit al ""\n"
				"	servidor. Se debe especificar un mensaje a la hora de hacer""\n"
				"	el commit.El server recibe los cambios, y cuando ha terminado""\n"
				"	de procesar los cambios, retorna un id	de commit al cliente""\n"
				"	generado con MD5.""\n"
				"\n"
				"	El server debe verificar que el commit del cliente esté al día""\n"
				"	con el resto de cambios realizados por otros usuarios. En caso""\n"
				"	de que no sea asi, rechaza el commit. " << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got status <file>:" "\n"
				"	Este comando nos va a mostrar cuales archivos han sido""\n"
				"	cambiados, agregados o eliminados de acuerdo con el commit""\n"
				"	anterior. Si el usuario especifica <file>, muestra el historial""\n"
				"	de cambios, recuperando el historial de	cambios desde el servidor" << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got rollback <file> <commit>:""\n"
				"	Permite regresar un archivo en el tiempo a un commit ""\n"
				"	especifico. Para esto, se comunica al servidor y recupera""\n"
				"	el archivo hasta dicha version" << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got reset <file>:""\n"
				"	Deshace cambios locales para un archivo y lo regresa al ultimo commit." << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << endl;
			cout << "got sync <file>:""\n"
				"	Recupera los cambios para un archivo en el servidor y""\n"
				"	lo sincroniza con el archivo en el cliente. Si hay cambios""\n"
				"	locales, debe permitirle de alguna forma, que el usuario ""\n"
				"	haga merge de los cambios interactivamente" << endl;
			cout << "_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ " << endl;
			cout << "_______________________________________________________________________ " << endl;
			continue;
		}



		if (comand.find("got add") != string::npos) {
			string name = getInLine(comand);
			if (!findFiles(repPath, name)) {
				cout << "El archivo ya ha sido agregado anteriormente o no existe, intente de nuevo correctamente" << endl;
				continue;
			} 
			if (directorioArchivos.size > 0) {
				cout << "Archivos listos para commit" << endl;
			}

			continue;

		}

		if (comand.find("got commit") != string::npos) {


			string message = getInLine(comand);
			string jsoncom = "{\"MessageCommit\":\""+message+"\",\"DateCommit\":\"2020-08-13T20:46:05.1468152-06:00\"}";

			auto c = cpr::Post(cpr::Url{ "https://localhost:44348/api/commited" },
				cpr::Body{ jsoncom },
				cpr::Header{ { "Content-Type", "application/json" } });

			struct json_object* tempjson2;
			struct json_object* tempjson3;
			json_object* parsedjson2 = json_tokener_parse((c.text).c_str());
			json_object_object_get_ex(parsedjson2, "idCommit", &tempjson2);
			json_object_object_get_ex(parsedjson2, "dateCommit", &tempjson3);
			int commitID = json_object_get_int(tempjson2);
			string date = json_object_get_string(tempjson3);

			std::cout << "Commit ID: " << commitID << std::endl;
			cout << "Fecha del commit: " << date << endl;
			
			auto rep = cpr::Get(cpr::Url{ "https://localhost:44348/api/repositorie/byname/" + repPath });

			struct json_object* repo;
			json_object* repositorie = json_tokener_parse((rep.text).c_str());
			json_object_object_get_ex(repositorie, "idRepositorie", &repo);
			int repID = json_object_get_int(repo);


			Node* temp = directorioArchivos.head;
			while (temp != nullptr) {
				string jsonfile = getFileJson(temp->dir, temp->name, temp->data, repID, commitID);

				auto a = cpr::Post(cpr::Url{ "https://localhost:44348/api/archivo" },
					cpr::Body{ jsonfile },
					cpr::Header{ { "Content-Type", "application/json" } });

				temp = temp->next;
			}
			cout << "Para el directorio " + repPath + " se ha realizado correctamente el commit de los siguientes archivos: " << endl;
			directorioArchivos.display(repPath);
			cout << "Commit: " + message << endl;

			directorioArchivos.clearList();
			continue;
		}

		if (comand.find("got status") != string::npos) {

			string filename = getInLine(comand);

			auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo/all/"+repPath+"/"+ filename});
			cout << endl;

			decode(f.text);


			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;


		}
		if (comand.find("got rollback") != string::npos) {

			string filename;
			string ids;
			int size = comand.size();
			int i = 0;
			int cont = 0;
			while (i < size) {
				if (cont > 0) {
					if (comand.substr(i, 1) == " ") {
						i++;
						while (comand.substr(i, 1) != " ") {
							filename += comand.substr(i, 1);
							i++;
						}
						i++;
						while (comand.substr(i, 1) != "") {
							ids += comand.substr(i, 1);
							i++;
						}
						break;
					}

				}
				else if (comand.substr(i, 1) == " ") {
					cont++;

				}
				i++;
			}
			stringstream toint(ids);
			int id;
			toint >> id;
			auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo/bycommit/"+filename+"/"+to_string(id)});
			struct json_object* tempjson;
			json_object* parsedjson = json_tokener_parse((f.text).c_str());
			json_object_object_get_ex(parsedjson, "dataArchivo", &tempjson);
			string datafile = json_object_get_string(tempjson);
			
			clearFile(repPath, filename);
			writeFile(repPath, filename, datafile);

			cout << "Se ha cambiado la data del archivo por el commit indicado"<< endl;
			continue;


		}
		if (comand.find("got reset") != string::npos) {
			
			string filename = getInLine(comand);

			auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo/" + repPath + "/" + filename });

			string searching = f.text;

			if (searching.find("dataArchivo") != string::npos) {
				struct json_object* tempjson;
				json_object* parsedjson = json_tokener_parse((f.text).c_str());
				json_object_object_get_ex(parsedjson, "dataArchivo", &tempjson);
				string datafile = json_object_get_string(tempjson);
				string datacompare = getFileData(repPath, filename);
				if (datacompare == datafile) {
					cout<<"El archivo actual ya posee la ultima version";
				}
				else {
					clearFile(repPath, filename);
					writeFile(repPath, filename,datafile);
				}
			}
			cout << "Se ha cambiado el archivo por el ultimo commit"<< endl;
			continue;
		}
		if (comand.find("got sync") != string::npos) {

			string filename = getInLine(comand);

			auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo/" + repPath + "/" + filename });
			string searching = f.text;

			if (searching.find("dataArchivo") != string::npos) {
				struct json_object* tempjson;
				json_object* parsedjson = json_tokener_parse((f.text).c_str());
				json_object_object_get_ex(parsedjson, "dataArchivo", &tempjson);
				string datafile = json_object_get_string(tempjson);
				string datacompare = getFileData(repPath, filename);
				if (datacompare == datafile) {
					cout << "El archivo actual ya posee la ultima version";
				}
				else {
					string option;
					cout << "Como le gustaria generar el merge?" << endl;
					cout << "Opcion numero 1: Ubicar primero informacion remota y seguida de esta ubicar informacion local" << endl;
					cout << "Opcion numero 2: Ubicar primero informacion local y seguida de esta ubicar informacion remota" << endl;
					cout << "Por favor digite el numero de la opcion deseada" << endl;
					getline(cin, option);
					string oldData = getFileData(repPath, filename);
					clearFile(repPath, filename);
					
					if (option=="1") {
						writeFile(repPath, filename, datafile  + "\n" + oldData);
					}
					else if (option == "2") {
						writeFile(repPath, filename, oldData + "\n" + datafile);
					}
				}
			}
			cout << "Se ha realizado el merge correctamente"<< endl;
			continue;
		}
		if (comand == "exit") {
			cout << "TERMINA APLICACION" << endl;
			break;
		}
		else {
			cout << "Comando Invalido" << endl;
			cout << "Por favor Ingrese Comando Disponible" << endl;
		}

	}
}

string ConsoleClient::getInLine(string comand){
	string line;
	int size = comand.size();
	int i = 0;
	int cont = 0;
	while (i < size) {
		if (cont > 0) {
			if (comand.substr(i, 1) == " ") {
				i++;
				while (comand.substr(i, 1) != "") {
					line += comand.substr(i, 1);
					i++;
				}
				break;
			}
			
		}
		else if (comand.substr(i, 1) == " ") {
			cont++;
			
		}
		i++;
	}
	return line;
}

string ConsoleClient::getFileData(string directorio,string name){
	string data;
	FILE* archivo;
	char linea[MAXLIN];
	archivo = fopen((directorio + "/"+ name).c_str(), "rb");
	if (archivo == NULL) {
		cout << "ERROR AL LEER O ENCONTRAR ARCHIVO, VUEgotLVA A INTENTAR DE MANERA CORRECTA" << endl;
		return "";
	}
	int cont = 1;
	while (fgets(linea, MAXLIN, archivo) != NULL) {
		data += linea;
	}
	fclose(archivo);
	return data;
}

string ConsoleClient::getFileJson(string direccion, string nombre, string data, int repID, int comID)
{
	json_object* jobjArchivo = json_object_new_object();
	
	json_object* jdir = json_object_new_string(direccion.c_str());
	
	json_object* jname = json_object_new_string(nombre.c_str());
	
	json_object* jdata = json_object_new_string(data.c_str());
	
	json_object* jidrepo = json_object_new_int(repID);

	json_object* jidcom = json_object_new_int(comID);
	
	json_object_object_add(jobjArchivo, "DireccionArchivo", jdir);
	
	json_object_object_add(jobjArchivo, "NombreArchivo", jname);

	json_object_object_add(jobjArchivo, "DataArchivo", jdata);
	
	json_object_object_add(jobjArchivo, "IDRepositorie", jidrepo);

	json_object_object_add(jobjArchivo, "IDCommit", jidcom);
	
	return json_object_to_json_string(jobjArchivo);
}

bool ConsoleClient::checkState(string repname, string filename){

	//std::cout << "Comparando Archivos" << std::endl;

	auto f = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo/" + repname + "/" + filename });
	
	string searching = f.text;

	if (searching.find("dataArchivo") != string::npos) {
		struct json_object* tempjson;
		json_object* parsedjson = json_tokener_parse((f.text).c_str());
		json_object_object_get_ex(parsedjson, "dataArchivo", &tempjson);
		string datafile = json_object_get_string(tempjson);
		string datacompare = getFileData(repname, filename);
		if (datacompare == datafile) {
			return false;
		}
	}
	return true;
}

void ConsoleClient::clearFile(string rep, string name)
{
	std::ofstream ofs;
	string path = rep + "/" + name;
	ofs.open(path, std::ofstream::out | std::ofstream::trunc);
	ofs.close();
}

void ConsoleClient::writeFile(string repPath, string name, string data){
	ofstream archivo;  // objeto de la clase ofstream

	archivo.open(repPath + "/" + name);
	archivo << data<< endl;
	archivo.close();
}


void run() {
	ConsoleClient* client = ConsoleClient::getInstance();
	client->ConsoleComand();
}



bool ConsoleClient::findFiles(string directorio,string name){
	DIR* dir;
	
	struct dirent* ent;
	dir = opendir(directorio.c_str());

	if (dir == NULL) {
		cout << ("No se logra abrir el directorio, vuelva a intentar correctamente");
		return false;
	}
	int cont = 0;
	std::cout << "Comparando Archivos" << std::endl;
	while ((ent = readdir(dir)) != NULL)
	{
		if (name == "-A") {
			if ((strcmp(ent->d_name, directorio.c_str()) != 0) && (strcmp(ent->d_name, "..") != 0) && (strcmp(ent->d_name, ".") != 0)) {
				string names = ent->d_name;
				string data = getFileData(directorio, names);
				if (data != "" && checkState(directorio,names)==true) {
					directorioArchivos.addNode(names, directorio, data);
					cont = 1;
				}
			}
		}
		else {
			if ((strcmp(ent->d_name, directorio.c_str()) != 0) && (strcmp(ent->d_name, "..") != 0) && (strcmp(ent->d_name, ".") != 0)) {
				if (ent->d_name == name) {
					string data = getFileData(directorio, name);
					if ( data != "" && checkState(directorio, name) == true) {
						directorioArchivos.addNode(name, directorio, data);
						cont = 1;
						break;
					}
				}
			}
		}

	}
	closedir(dir);
	if (cont == 0) {
		return false;
	}
	return true;
}

void ConsoleClient::decode(string json){
	

	Json::CharReaderBuilder builder;
	Json::CharReader* reader = builder.newCharReader();

	Json::Value root;
	string errors;

	bool parsingSuccessful = reader->parse(json.c_str(), json.c_str() + json.size(), &root, &errors);
	delete reader;

	if (!parsingSuccessful)
	{
		cout << json << endl;
		cout << errors << endl;
	}

	for (Json::Value::const_iterator outer = root.begin(); outer != root.end(); outer++)
	{
		for (Json::Value::const_iterator inner = (*outer).begin(); inner != (*outer).end(); inner++)
		{
			cout << inner.key() << ": " << *inner << endl;
		}
		cout << endl;
	}
}
