from pylsl import StreamInlet, resolve_stream, StreamInfo, StreamOutlet
import csv

def main():
    # first resolve a marker stream on the lab network
    print("looking for a marker stream...")
    streams = resolve_stream('type', 'Markers')

    # create a new inlet to read from the stream
    inlet = StreamInlet(streams[0])
    
    filename = "ShellGame.csv"
    header = ['Time', 'Ray Origin', 'Direction', 'Object Tag', 'Event']
    with open(filename, 'w', newline="") as file:
        csvwriter = csv.writer(file)
        csvwriter.writerow(header)

    timeout_duration = 5.0  # Set timeout duration to 5 seconds
    while True:
        # get a new sample with timeout (e.g., 5 seconds)
        sample, timestamp = inlet.pull_sample(timeout=timeout_duration)

        if sample is None:
            # If no sample is received within the timeout, break the loop
            print("No sample received within the timeout. Exiting...")
            break

        # Split sample by / character
        sampleList = sample[0].split('/')
        # If sample is eye tracking data, print info to console and record ray origin, direction of eyes, and object being looked at
        if len(sampleList) > 1:
            print("%s: Ray Origin: %s / Direction: %s / Object Tag: %s" % (timestamp, sampleList[0], sampleList[1], sampleList[2]))
            with open(filename, 'a', newline="") as file:
                csvwriter = csv.writer(file)
                csvwriter.writerow([timestamp, sampleList[0], sampleList[1], sampleList[2]])
        # If sample is just an event tag, print that to conosle and record the tag in the event column
        else:
            print("%s: %s" % (timestamp, sampleList[0]))
            with open(filename, 'a', newline="") as file:
                csvwriter = csv.writer(file)
                csvwriter.writerow([timestamp, '', '', '', sampleList[0]])

if __name__ == '__main__':
    main()
