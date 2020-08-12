/**
* @file ConsoleClient.h
* @version 1.0
* @date 4/08/2020
* @author Mario Gudino Rovira
* @title ConsoleClient
* @brief Cliente de la aplicacion
*/
#pragma once
#include <iostream>
#include <thread>
#include <string>
#include <cstdio>
#include <cstdlib>
#include <direct.h>
#include <fstream>
#include <vector>
#include <string>
#include <iostream>
#include "../../../../dirent-master/include/dirent.h"
#include "../LinkedList/LinkedList.h"

#define MAXLIN 8000

using namespace std;



class ConsoleClient{
public:
	thread thrd;

	LinkedList directorioArchivos;

	/**
	* @brief getInstance obtiene o crea singleton cliente
	* @return referencia instanciada de la clase
	*/
	static ConsoleClient* getInstance();

	/**
	* @brief findFiles encuentra los archivos de un directorio especifico y de encontrarlos los guarda en una linkedlist
	* @param directorio string con path del directorio a buscar
	* @param name archivo que se quiere buscar dentro del directorio
	* @return bool que indica si se encuentra el archivo name
	*/
	bool findFiles(string directorio,string name);

	/**
	* @brief ConsoleComando metodo que ejecuta la aplicacione en consola
	*/
	void ConsoleComand();

	/**
	* @brief getInLine obtiene linea que se escribe despues de comando en consola
	* @param comand string que se escribe
	* @return string con el comando que ingresa el usuario <comando>
	*/
	string getInLine(string comand);

	/**
	* @brief getFileData obtiene la data de algun archivo especifico
	* @param directorio string con el nombre del directorio en el cual esta el archivo
	* @param name string con el nombre del archivo que sea desea buscar la data
	*/
	string getFileData(string directorio,string name);

private:
	/**
	* @brief ConsoleClient constructor de la clase
	*/
	ConsoleClient();
	static ConsoleClient* instance;
};

