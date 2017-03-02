#/usr/bin/env python
# -*- coding: utf-8 -*-

import subprocess

# set path
result = subprocess.check_output("calcmd 1+2+3+4")
print("result:{}".format(result))
