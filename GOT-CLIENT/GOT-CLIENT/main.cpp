
#include<iostream>
#include "ConsoleClient.h"

using namespace std;

int main() {
	ConsoleClient* client = ConsoleClient::getInstance();
	client->thrd.join();
	return 0;
}