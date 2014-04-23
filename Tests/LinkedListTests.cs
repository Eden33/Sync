﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedList;
using NUnit.Framework;
using Graph.Model;

namespace Graph.Test
{
    [TestFixture(Description="Search a specific item in the linked list implementation."
    + " Search can be done in both directions. This is a simple and rather static test.")]
    public class LinkedListSimpleSearchTests
    {
        Node[] nodes = null;
        int numItems = 100;
        
        [TestFixtureSetUp]
        public void SetUpNodes()
        {
            nodes = new Node[numItems];
            nodes[0] = new Node(0);

            for (int i = 1; i < numItems; i++)
            {
                nodes[i] = nodes[0].InsertAtEnd(new Node(i));
            }
        }

        [Test]
        public void testSearchNext()
        {
            int searchVal = (int) (numItems / 1.3 );
            int searchId = JunctionPoint.SearchBuffer.InitSearchBuffer();
            INode found = nodes[numItems / 2].FindNext(searchVal, searchId);
            JunctionPoint.SearchBuffer.ClearSearchBuffer(searchId);
            JunctionPoint.SearchBuffer.ResetSearchBuffer();
            Assert.IsNotNull(found);
            Assert.AreEqual(found.Id, searchVal);
        }

        [Test]
        public void testSearchPrev()
        {
            int searchVal = (int)(numItems / 3);
            int searchId = JunctionPoint.SearchBuffer.InitSearchBuffer();
            INode found = nodes[numItems / 2].FindPrev(searchVal, searchId);
            JunctionPoint.SearchBuffer.ClearSearchBuffer(searchId);
            JunctionPoint.SearchBuffer.ResetSearchBuffer();
            Assert.IsNotNull(found);
            Assert.AreEqual(found.Id, searchVal);
        }
    }
}
