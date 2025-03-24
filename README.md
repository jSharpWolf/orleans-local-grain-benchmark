## Run the benchmarks


Execute the powershell "./run.ps1" script

Invoke the "bench" endpoint with the following parameters:

http://localhost:35550/bench?duration=3&depth=10&mode=2

| Parameter | Description |
| -- | -- |
| Duration | Duration of the run in seconds |
| Depth | How many child grains should be created |
| Mode | Grain Mode (0 = Random, 1 = PreferLocal, 2 = StatelessWorker, 3 = Naive Grain Directory, 4 = Experimental Distributed Grain Directory ) |  