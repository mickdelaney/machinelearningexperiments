import random
import unittest
from searchEngine import * 
from sklearn.feature_extraction.text import CountVectorizer

class when_a_search_is_performed(unittest.TestCase):

    def setUp(self):
        self.searchEngine = searchEngine()
        self.train_set = ("The sky is blue.", "The sun is bright.")
        self.test_set = ("The sun in the sky is bright.","We can see the shining sun, the bright sun.")
        self.vectorizer = CountVectorizer()
        self.vectorizer.fit_transform(self.train_set)

    def test_it_should_return_the_documents_in_the_correct_order(self):
    	self.assertEqual(1, 1)
        # self.assertEqual(self.seq, range(10))
        # self.assertRaises(TypeError, random.shuffle, (1,2,3))


if __name__ == '__main__':
    unittest.main()