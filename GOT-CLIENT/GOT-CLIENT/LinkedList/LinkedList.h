/**
* @file LinkedList.h
* @version 1.0
* @date 4/08/2020
* @author Mario Gudino Rovira
* @title LinkedList
* @brief Lista Enlazada
*/
#pragma once
#include <iostream>
#include "Node.h"
using namespace std;


class LinkedList{
public:
	Node* head;
	Node* tail;
	int size;

	/**
	* @brief addNode agregar un nuevo nodo a la lista
	* @param name string que inidica nombre del archivo al cual se desea guardar informacion
	* @param directorio string inidica nombre del directorio
	* @param data string que guarda informacion de un archivo
	*/
	void addNode(string name, string directorio, string data);

	/**
	* @brief LinkedList constructor de la clase
	*/
	LinkedList();

	/**
	* @brief display muestra el nombre de los archivos guardados en algun directorio especifico
	* @param directorio nombre del directorio al cual se quiere buscar archivos
	*/
	void display(string directorio);

	/**
	* @brief removeNode elimina un nodo de la lista
	* @param directorio inidica nombre del directorio que tiene asociado el nodo al que se quiere eliminar
	* @param name indica el nombre del nodo que se quiere eliminar
	*/
	void removeNode(string directorio, string name);

	/**
	* @brief isInList inidica si la lista contiene un nodo especifico
	* @param directorio inidica nombre del directorio que tiene asociado el nodo 
	* @param name indica el nombre del nodo que se quiere buscar
	*/
	bool isInList(string directorio,string name);
};

