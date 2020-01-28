# Expert system - resources allocation problem

My implementation of simplified verion of problem described in details [Resource allocation problem description](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC6112644/)

## Problem description
There is a set of experts, each has a certain set of skills. We have a set of projects each requires some set of skills. How to calculate the best fit expert to project so that the most number of experts is allocated to a project?

### Solution
Create flow graph with experts, skills and projects as edges as shown on figure (each expert can be connected to project "via" skill).
![https://raw.githubusercontent.com/lucky125111/ExpertSystem/master/ExpertSystem.png](https://raw.githubusercontent.com/lucky125111/ExpertSystem/master/ExpertSystem.png)
Calculating flow in such graph will result with the biggest number of experts allocated to projects.

## Running from command line
`ResourceAllocation.exe  `**`path_to_input_file`**

## Projects/Folders in solution
* ResourceAllocation - console application project
* AllocationService - project where all logic is places 
	* Creating graph from input
	* Residual graph creation
	* Calculating max flow
* Tests - units test project
* TestsCases - example inputs for testing



