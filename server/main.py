import json
import itertools
import collections
import math

import flask
import requests
import sentry_sdk

_DATA_PREFIX = "data:"

sentry_sdk.init(
    # DSN configured as envvar in google cloud run
    traces_sample_rate=1.0
)

def main(request):
    max_lines = int(request.args.get("max_lines", 1000))
    upstream_response = requests.get(
        "https://live.sentry.io/stream",
        stream=True
    )

    upstream_response.raise_for_status()

    aggregates = collections.defaultdict(lambda: 0)

    for line in itertools.islice(upstream_response.iter_lines(), 0, max_lines):
        line = line.decode("ascii")

        if not line.startswith(_DATA_PREFIX):
            continue

        lat, lon, timestamp, platform = json.loads(line[len(_DATA_PREFIX):])


        length = math.sqrt(lat ** 2 + lon ** 2)
        lat /= length
        lon /= length

        # Reduce number of aggregates somehow, we just need some approximate
        # direction
        lat = round(lat, 1)
        lon = round(lon, 1)

        key = (lat, lon, platform)

        aggregates[key] += 1

    response = [
        {
            "lat": lat,
            "lon": lon,
            "platform": platform,
            "count": count
        }
        for (lat, lon, platform), count in aggregates.items()
    ]

    return flask.jsonify(response)
