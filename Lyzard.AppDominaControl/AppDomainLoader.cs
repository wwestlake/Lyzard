﻿using AppDomainToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.AppDominaControl
{

    /// <summary>
    /// Manages an AppDomain
    /// </summary>
    public class AppDomainLoader
    {
        private AppDomainContext<AssemblyTargetLoader, PathBasedAssemblyResolver> _context;
        private bool _loaded;

        public bool IsLoaded { get => _loaded; private set => _loaded = value; }

        public AppDomainLoader(string appName, string dllpath)
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var setupInfo = new AppDomainSetup()
            {
                ApplicationName = appName,
                ApplicationBase = rootDir,
                PrivateBinPath = rootDir,
            };
            _context = AppDomainContext.Create(setupInfo);
            _context.LoadAssembly(LoadMethod.LoadFile, dllpath);
            IsLoaded = true;
        }

        public AppDomainLoader(string appName, IEnumerable<string> dllpaths)
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var setupInfo = new AppDomainSetup()
            {
                ApplicationName = appName,
                ApplicationBase = rootDir,
                PrivateBinPath = rootDir,
            };
            _context = AppDomainContext.Create(setupInfo);
            foreach (var dllpath in dllpaths)
            {
                _context.LoadAssembly(LoadMethod.LoadFile, dllpath);
            }
            IsLoaded = true;
        }


        public void RunRemoteAction(Action action)
        {
            RemoteAction.Invoke(_context.Domain, action);
        }

        public async Task RunRemoteActionAsync(Action action)
        {
            await Task.Factory.StartNew(() =>
               RemoteAction.Invoke(_context.Domain, action)
            );
        }

        public void RunRemoteAction<T>(T arg, Action<T> action)
        {
            RemoteAction.Invoke<T>(_context.Domain, arg, action);
        }

        public async Task RunRemoteActionAsync<T>(T arg, Action<T> action)
        {
            await Task.Factory.StartNew(() =>
                RemoteAction.Invoke<T>(_context.Domain, arg, action)
            );
        }

        public void RunRemoteAction<T1,T2>(T1 arg1, T2 arg2, Action<T1, T2> action)
        {
            RemoteAction.Invoke<T1,T2>(_context.Domain, arg1, arg2, action);
        }

        public async Task  RunRemoteActionAsync<T1, T2>(T1 arg1, T2 arg2, Action<T1, T2> action)
        {
            await Task.Factory.StartNew(() =>
                RemoteAction.Invoke<T1, T2>(_context.Domain, arg1, arg2, action)
            );
        }

        public void RunRemoteAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Action<T1, T2, T3> action)
        {
            RemoteAction.Invoke<T1, T2, T3>(_context.Domain, arg1, arg2, arg3, action);
        }

        public async Task RunRemoteActionAsync<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Action<T1, T2, T3> action)
        {
            await Task.Factory.StartNew(() =>
                RemoteAction.Invoke<T1, T2, T3>(_context.Domain, arg1, arg2, arg3, action)
            );
        }

        public void RunRemoteAction<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<T1, T2, T3, T4> action)
        {
            RemoteAction.Invoke<T1, T2, T3, T4>(_context.Domain, arg1, arg2, arg3, arg4, action);
        }

        public async Task RunRemoteActionAsync<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<T1, T2, T3, T4> action)
        {
            await Task.Factory.StartNew(() =>
                RemoteAction.Invoke<T1, T2, T3, T4>(_context.Domain, arg1, arg2, arg3, arg4, action)
            );
        }

        public Tr RunRemoteFunc<Tr>(Func<Tr> func)
        {
            return RemoteFunc.Invoke(_context.Domain, func);
        }

        public async Task<Tr> RunRemoteFuncAsync<Tr>(Func<Tr> func)
        {
            return await Task.Factory.StartNew(() =>
                RemoteFunc.Invoke(_context.Domain, func)
            );
        }

        public Tr RunRemoteFunc<Tr, T>(T arg, Func<T, Tr> func)
        {
            return RemoteFunc.Invoke<T, Tr>(_context.Domain, arg, func);
        }

        public async Task<Tr> RunRemoteFuncAsync<Tr, T>(T arg, Func<T, Tr> func)
        {
            return await Task.Factory.StartNew(() =>
                RemoteFunc.Invoke<T, Tr>(_context.Domain, arg, func)
            );
        }

        public Tr RunRemoteFunc<Tr, T1, T2>(T1 arg1, T2 arg2, Func<T1, T2, Tr> func)
        {
            return RemoteFunc.Invoke<T1, T2, Tr>(_context.Domain, arg1, arg2, func);
        }

        public async Task<Tr> RunRemoteFuncAsync<Tr, T1, T2>(T1 arg1, T2 arg2, Func<T1, T2, Tr> func)
        {
            return await Task.Factory.StartNew(() =>
                RemoteFunc.Invoke<T1, T2, Tr>(_context.Domain, arg1, arg2, func)
            );
        }

        public Tr RunRemoteFunc<Tr, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Func<T1, T2, T3, Tr> func)
        {
            return RemoteFunc.Invoke<T1, T2, T3, Tr>(_context.Domain, arg1, arg2, arg3, func);
        }

        public async Task<Tr> RunRemoteFuncAsync<Tr, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Func<T1, T2, T3, Tr> func)
        {
            return await Task.Factory.StartNew(() =>
                RemoteFunc.Invoke<T1, T2, T3, Tr>(_context.Domain, arg1, arg2, arg3, func)
            );
        }

        public Tr RunRemoteFunc<Tr, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<T1, T2, T3, T4, Tr> func)
        {
            return RemoteFunc.Invoke<T1, T2, T3, T4, Tr>(_context.Domain, arg1, arg2, arg3, arg4, func);
        }

        public async Task<Tr> RunRemoteFuncAsync<Tr, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<T1, T2, T3, T4, Tr> func)
        {
            return await Task.Factory.StartNew(() =>
                RemoteFunc.Invoke<T1, T2, T3, T4, Tr>(_context.Domain, arg1, arg2, arg3, arg4, func)
            );
        }

        public void Unload()
        {
            AppDomain.Unload(_context.Domain);
            IsLoaded = false;
        }

    }



}
