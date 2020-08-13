/**
* @file Node.h
* @version 1.0
* @date 4/08/2020
* @author Mario Gudino Rovira
* @title Node
* @brief Nodo de lista enlazada
*/
#pragma once
#include <iostream>

using namespace std;

class Node {

public:
	string data;
	string directorio;
	string name;
	string dir;
	Node* next;

	/**
	* @brief Node constructor de la clase
	*/
	Node();
};

