__author__ = 'mickdelaney'

import sys
import json
from sklearn.feature_extraction.text import CountVectorizer

class searchEngine: 
	def __init__(self):
		self.vectorizer = CountVectorizer()

	def vectorize(self, words):
		self.words = words

class searchResult:
    def __init__(self, data):
        self.data = data

